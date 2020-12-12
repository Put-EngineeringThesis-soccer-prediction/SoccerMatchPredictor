from data_fetcher import DataFetcher
from preprocessor import Preprocessor


class DataAggregator:

    def __init__(self, preprocessor=Preprocessor(), data_fetcher=DataFetcher()):
        self.preprocessor = preprocessor
        self.data_fetcher = data_fetcher

    def get_data_for_seasons(self, seasons, params):
        data = []

        for season in seasons:
            season_data = self.data_fetcher.fetch_data_for_season(season, params)
            data.append(season_data)

        return self.preprocessor.process_data(data)

    def get_data_for_team_in_seasons(self, team, seasons, params):
        data = []

        for season in seasons:
            season_data = self.data_fetcher.fetch_data_for_team_in_season(season, team, params)
            data.append(season_data)

        return self.preprocessor.process_data(data)
