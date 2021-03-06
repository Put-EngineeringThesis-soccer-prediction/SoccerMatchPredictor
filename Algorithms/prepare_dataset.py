from sklearn.utils import resample
from collections import Counter
import pandas as pd
import numpy as np
import sys
from pathlib import Path
import os
from multipledispatch import dispatch
from multi_imbalance.resampling.global_cs import GlobalCS
import warnings

class BadSplitSize(Exception):
    """Raise an error if test_size is incorrect."""
    def __init__(self, message = None, errors = None):
        super().__init__(message)
        self.errors = errors

@dispatch(int, str, int)
def get_direct_matches_by_id(match_id, data_path, avg):
    match = pd.read_csv(os.path.join(data_path, f'{match_id}_{avg}.csv'), index_col=0)
    match['match_date'] = pd.to_datetime(match['match_date'])
    return match

@dispatch(int, dict, int)
def get_direct_matches_by_id(match_id, all_data_past, avg):
    match = all_data_past[match_id]
    match['match_date'] = pd.to_datetime(match['match_date'])
    return match

def data_undersample (X, y, id_):
    classes = Counter(y)
    
    df = pd.concat([id_, X, y], axis=1)
    df_majority = df[df['match_result'] == classes.most_common()[0][0]]
    df_minority = df[df['match_result'] == classes.most_common()[-1][0]]
    df_middle = df[df['match_result'] == classes.most_common()[1][0]]
    
    
    df_majority_undersample = resample(df_majority, 
                                replace=False,    # sample without replacement
                                n_samples=len(df_middle),  # to match minority class
                                random_state=1234) # reproducible results
    df_undersample = pd.concat([df_majority_undersample, df_minority, df_middle])
    df_undersample = df_undersample.sort_values(by=['match_id'])
    return df_undersample.iloc[:,1:-1], df_undersample['match_result']

def data_equalize(X, y):
    X, y = GlobalCS().fit_resample(np.copy(X), np.copy(y))
    return pd.DataFrame(X), pd.Series(y)

def add_direct_matches(dataset, data_path, avg):
    home_wins_list = list()
    away_wins_list = list()
    draws = list()
    for i, match_id in enumerate(dataset['match_id']):
        direct_matches = get_direct_matches_by_id(match_id, data_path, avg)
        home_wins = 0
        away_wins = 0
        draw = 0
        for index in direct_matches.index:
            home_team = direct_matches['home_team_id'][index]
            result = direct_matches['match_result'][index]   
            if (home_team == dataset['teams_names'].home_team_id[i] and result == 1) or \
            (home_team == dataset['teams_names'].away_team_id[i] and result == 2):
                home_wins += 1
            elif (home_team == dataset['teams_names'].home_team_id[i] and result == 2) or \
                (home_team == dataset['teams_names'].away_team_id[i] and result == 1):
                away_wins += 1
            else:
                draw += 1
        home_wins_list.append(home_wins)
        away_wins_list.append(away_wins)
        draws.append(draw)
    dataset['X'].loc[:, 'home_direct_wins'] = home_wins_list
    dataset['X'].loc[:, 'away_direct_wins'] = away_wins_list
    dataset['X'].loc[:, 'direct_draws'] = draws
    

def prepare_dataset(data, list_of_parameters, data_path,  add_direct = False, avg = 3, train_size = 1.0, test_size = 0.0, undersample  = True, globalCS = False):
    warnings.simplefilter("ignore")
    dataset = dict()
    #data.sort_values(by=['match_date'])
    dataset['match_id'] = data['match_id']
    dataset['teams_names'] = data[['home_team_id', 'away_team_id', 'home_team_name','away_team_name']]
    dataset['match_date'] = data['match_date']
    dataset['y'] = data['match_result']
    dataset['X'] = data[list_of_parameters]
    
    if train_size < 0.0 or train_size > 1.0:
        raise BadSplitSize("Podano błędną wartość 'train_size'.")
    if test_size < 0.0 or test_size > 1.0:
        raise BadSplitSize("Podano błędną wartość 'test_size'.")
    if train_size + test_size > 1.0:
        raise BadSplitSize("Podano błędną wartość 'train_size' oraz 'test_size.")

    train_size = int(train_size * 100)
    test_size = int(test_size * 100)
    if add_direct:
        add_direct_matches(dataset, data_path, avg)

    if undersample:
        dataset['X'], dataset['y'] = data_undersample(dataset['X'], dataset['y'], dataset['match_id'])
    
    temp = len(dataset['X']) * train_size // 100
    temp_test = len(dataset['X']) * (100 - test_size) // 100

    dataset['X_train'] = dataset['X'][:temp]
    dataset['y_train'] = dataset['y'][:temp]

    dataset['X_valid'] = dataset['X'][temp: temp_test]
    dataset['y_valid'] = dataset['y'][temp: temp_test]
        
    dataset['X_test'] = dataset['X'][temp_test:]
    dataset['y_test'] = dataset['y'][temp_test:]

    if globalCS:
        dataset['X_train'], dataset['y_train'] = data_equalize(dataset['X_train'], dataset['y_train'])

    return dataset