import requests
from api import get_url_for_matches_in_season
from api import get_url_for_team_matches_in_season


class DataFetcher:

    def __init__(self):
        self.cache = {}

    def fetch_data_for_season(self, season, params):
        url = get_url_for_matches_in_season(season.value, params)
        data = requests.get(url).text
        self.cache[url] = data
        return data

    def fetch_data_for_team_in_season(self, season, team, params):
        url = get_url_for_team_matches_in_season(season.value, team.value, params)
        data = requests.get(url).text
        self.cache[url] = data
        return data
