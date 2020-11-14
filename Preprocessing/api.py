from enum import Enum

BASE_URL = "https://inzeuw-pp-footballdataapi.azurewebsites.net/api"
CODE_PARAM = "?code=YZnYml6vcWT1iAgnaBnMl7G8hg7PtytWldw1u2c/X3RBxuUxOgMp0A=="


class Endpoint(Enum):
    MATCHES_IN_SEASON = "/Matches/Season/DetailedMatch/"
    MATCH_DETAILS = "/Matches/"
    TEAM_HISTORY = "/Matches/"


def get_url_for_matches_in_season(season, params):
    return BASE_URL + Endpoint.MATCHES_IN_SEASON.value + f"{season}/{params.no_last_matches}" + CODE_PARAM


def get_url_for_team_matches_in_season(season, team, params):
    return BASE_URL + Endpoint.MATCHES_IN_SEASON.value + f"{season}/{team}/{params.no_last_matches}" + CODE_PARAM
