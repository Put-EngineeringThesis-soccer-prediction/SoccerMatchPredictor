from sklearn.utils import resample
from collections import Counter
import pandas as pd
import numpy as np
import sys
from pathlib import Path
import os
from multipledispatch import dispatch


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

def data_unsample(X, y, id_):
    classes = Counter(y)
    
    df = pd.concat([id_, X, y], axis=1)
    df_majority = df[df['match_result'] == classes.most_common()[0][0]]
    df_minority = df[df['match_result'] == classes.most_common()[-1][0]]
    df_middle = df[df['match_result'] == classes.most_common()[1][0]]
    
    
    df_majority_downsampled = resample(df_majority, 
                                replace=False,    # sample without replacement
                                n_samples=len(df_middle),  # to match minority class
                                random_state=1234) # reproducible results
    df_downsampled = pd.concat([df_majority_downsampled, df_minority, df_middle])
    df_downsampled = df_downsampled.sort_values(by=['match_id'])
    return df_downsampled.iloc[:,1:-1], df_downsampled['match_result']

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
    

def prepare_dataset(data, list_of_parameters, data_path,  add_direct = False, avg = 3, unsample = True):
    dataset = dict()
    #data.sort_values(by=['match_date'])
    dataset['match_id'] = data['match_id']
    dataset['teams_names'] = data[['home_team_id', 'away_team_id', 'home_team_name','away_team_name']]
    dataset['match_date'] = data['match_date']
    dataset['y'] = data['match_result']
    dataset['X'] = data[list_of_parameters]
    
    if add_direct:
        add_direct_matches(dataset, data_path, avg)
    if unsample:
        dataset['X'], dataset['y'] = data_unsample(dataset['X'], dataset['y'], dataset['match_id'])
    return dataset