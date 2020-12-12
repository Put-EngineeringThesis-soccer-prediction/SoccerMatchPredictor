import os
import sys
import unittest

sys.path.append(os.path.abspath('..'))
from parameters import Parameters
from api import get_url_for_matches_in_season, get_url_for_team_matches_in_season


class TestUrlGeneration(unittest.TestCase):

    def test_url_for_matches_in_season(self):
        season = '2015'
        parameters = Parameters(2)

        actual = get_url_for_matches_in_season(season, parameters)
        expected = 'https://inzeuw-pp-footballdataapi.azurewebsites.net/api/Matches/Season/DetailedMatch/2015/2?code' \
                   '=YZnYml6vcWT1iAgnaBnMl7G8hg7PtytWldw1u2c/X3RBxuUxOgMp0A=='

        self.assertEqual(actual, expected)

    def test_url_for_team_matches_in_season(self):
        season = '2015'
        team = '3459'
        parameters = Parameters(2)

        actual = get_url_for_team_matches_in_season(season, team, parameters)
        expected = 'https://inzeuw-pp-footballdataapi.azurewebsites.net/api/Matches/Season/DetailedMatch/2015/3459/2' \
                   '?code=YZnYml6vcWT1iAgnaBnMl7G8hg7PtytWldw1u2c/X3RBxuUxOgMp0A=='

        self.assertEqual(actual, expected)


if __name__ == '__main__':
    unittest.main()
