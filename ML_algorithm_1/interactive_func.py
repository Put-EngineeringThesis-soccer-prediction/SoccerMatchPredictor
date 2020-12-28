import os
import pandas as pd
import numpy as np
import sys
from pathlib import Path
import ipywidgets as widgets
from ipywidgets import interact, interact_manual
import pickle
from sklearn.preprocessing import StandardScaler
import warnings


sys.path.append(os.path.join(Path(os.getcwd()).parent, 'Preprocessing\\'))

from prepare_dataset import *
from dense_model import *
from data_aggregator import *
from season import *
from parameters import *
from team import *

LIST_OF_PARAMETERS = ['home_team_score',
                                'away_team_score', 
                                'home_team_seasons_played',
                                'away_team_seasons_played', 
                                'home_team_last_season_points',
                                'away_team_last_season_points', 
                                'home_players_avg_age',
                                'away_players_avg_age', 
                                'home_players_avg_rating',
                                'away_players_avg_rating', 
                                'home_elo_rating', 
                                'away_elo_rating',
                                'avg_home_win_odds', 
                                'avg_draw_odds', 
                                'avg_away_win_odds',
                                'home_avg_corners', 
                                'away_avg_corners', 
                                'home_avg_shots',
                                'away_avg_shots', 
                                'home_won_games', 
                                'away_won_games', 
                                'home_tied_games',
                                'away_tied_games', 
                                'home_lost_games', 
                                'away_lost_games',
                                'home_scored_goals', 
                                'away_scored_goals'
                            ]


class Interactive():
    def __init__(self):
        self.dataset = None
        self.list_of_parameters = LIST_OF_PARAMETERS
        self.lista = list()
        self.model_list = ['DenseNetwork', "SVM", "All"]


    def _get_teams_list_for_interactive(self):
        self.lista += list(Counter(self.dataset['teams_names']['home_team_name']).keys())
        self.lista = sorted(list(set(self.lista)))

    def get_data_from_database(self, last_matches = 3):
        warnings.simplefilter("ignore")
        data_aggregator = DataAggregator()
        all_data, all_data_past = data_aggregator.get_data_for_seasons([Season.y2010, Season.y2011, \
                                                         Season.y2012, Season.y2013, \
                                                         Season.y2014, Season.y2015, Season.y2016], \
                                                         Parameters(no_last_matches=last_matches))
        self.dataset = prepare_dataset(all_data, self.list_of_parameters, all_data_past, add_direct = True, avg = 3, undersample = False, globalCS = False)
        self._get_teams_list_for_interactive()

    def get_data_from_local(self, *, name = 'all_seasons_3.csv', data_dir = 'Dane', avg = 3):
        warnings.simplefilter("ignore")
        working_dir = Path(os.getcwd()).parent
        data_path = os.path.join(working_dir.parent, data_dir)
        all_seasons = pd.read_csv(os.path.join(data_path, name), index_col=0) 
        all_seasons['match_date'] = pd.to_datetime(all_seasons['match_date'])

        self.dataset = prepare_dataset(all_seasons, self.list_of_parameters, str(data_path), add_direct = True, avg = avg, undersample = False, globalCS = False)
        self._get_teams_list_for_interactive()

    def _get_data_for_model(self, date, home_team, away_team):
        indexes = np.where(pd.to_datetime(self.dataset['match_date']) == pd.Timestamp(date))[0]
        home_indexes = np.where(self.dataset['teams_names']['home_team_name'] == home_team)[0]
        away_indexes = np.where(self.dataset['teams_names']['away_team_name'] == away_team)[0]
        for index in indexes:
            if index in home_indexes and index in away_indexes:
                break 
        else:
            print('\nNiestety, taki mecz się nie odbędzie.')
            return []
        return np.array(self.dataset['X'].iloc[index - 1])[0:]

    def _check_teams(self, date):
        teams = self.dataset['teams_names'][pd.to_datetime(self.dataset['match_date']) == pd.Timestamp(date)].iloc[:, [2,3]]
        if teams.empty:
            print("\nTego dnia mecze się nie odbędą")
        else:
            print(f"\nDrużyny które rozegrają mecze tej daty to: \n\n{teams}")

    
    def compute_result(self, date, home_team = 'Manchester United', away_team = 'Bournemouth', model_str = 'DenseNetwork'):
        self._check_teams(date)
        data = self._get_data_for_model(date, home_team, away_team)
        if data == []:
            return

        output = {0: 'Draw', 1: home_team, 2: away_team}
        if model_str == 'DenseNetwork':
            data = np.expand_dims(data, axis=0)
            model = get_model()
            model.load_weights("net_weigths.h5")
            y_probas_dense = np.stack([model(data) for sample in range(100)])
            y_proba_dense = y_probas_dense.mean(axis=0)
            print(f"\n\nDenseNetwork probability: {y_proba_dense[0]}")
            result = np.argmax(y_proba_dense)
            
        elif model_str == 'SVM':
            data = np.expand_dims(data, axis=0)
            scaler = StandardScaler()
            with open('SVM_scaler.pkl', 'rb') as f:
                scaler = pickle.load(f) 
            with open('SVM.pkl', 'rb') as f:
                model = pickle.load(f)
                
            data = scaler.transform(data)
            result = model.predict(data)[0]
        print(f"\n-------\nModel output: {output[result]}\n-------")
        return 







