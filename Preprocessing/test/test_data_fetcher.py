import os
import sys
import unittest
import responses

sys.path.append(os.path.abspath('..'))
from parameters import Parameters
from season import Season
from team import Team
from data_fetcher import DataFetcher


class TestDataFetcher(unittest.TestCase):

    @responses.activate
    def test_fetch_data_for_season(self):
        season = Season.y2015
        parameters = Parameters(2)

        # mocking the response
        responses.add(responses.GET,
                      'https://inzeuw-pp-footballdataapi.azurewebsites.net/api/Matches/Season/DetailedMatch/2015/2'
                      '?code=YZnYml6vcWT1iAgnaBnMl7G8hg7PtytWldw1u2c/X3RBxuUxOgMp0A==', body='test_response')

        data_fetcher = DataFetcher()
        actual = data_fetcher.fetch_data_for_season(season, parameters)
        expected = 'test_response'

        self.assertEqual(actual, expected)

    @responses.activate
    def test_fetch_data_for_team_in_season(self):
        season = Season.y2015
        team = Team.Arsenal
        parameters = Parameters(2)

        # mocking the response
        responses.add(responses.GET,
                      'https://inzeuw-pp-footballdataapi.azurewebsites.net/api/Matches/Season/DetailedMatch/2015/3459/2'
                      '?code=YZnYml6vcWT1iAgnaBnMl7G8hg7PtytWldw1u2c/X3RBxuUxOgMp0A==', body='test_response')

        data_fetcher = DataFetcher()
        actual = data_fetcher.fetch_data_for_team_in_season(season, team, parameters)
        expected = 'test_response'

        self.assertEqual(actual, expected)

    def test_cache_usage(self):
        season = Season.y2015
        parameters = Parameters(2)

        data_fetcher = DataFetcher()
        # populating the internal cache
        data_fetcher.cache['https://inzeuw-pp-footballdataapi.azurewebsites.net/api/Matches/Season/DetailedMatch/2015' \
                           '/2?code=YZnYml6vcWT1iAgnaBnMl7G8hg7PtytWldw1u2c/X3RBxuUxOgMp0A=='] = 'test_response'

        actual = data_fetcher.fetch_data_for_season(season, parameters)
        expected = 'test_response'

        self.assertEqual(actual, expected)


if __name__ == '__main__':
    unittest.main()
