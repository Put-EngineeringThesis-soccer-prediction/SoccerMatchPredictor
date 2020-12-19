import os
import sys
import unittest
from unittest.mock import *

sys.path.append(os.path.abspath('..'))
from parameters import Parameters
from data_aggregator import DataAggregator
from season import Season
from team import Team


class TestDataAggregator(unittest.TestCase):

    def test_get_data_for_seasons(self):
        fetcher_mock = Mock()
        fetcher_mock.fetch_data_for_season = MagicMock(side_effect=lambda season, param: season.value)

        preprocessor_mock = Mock()
        preprocessor_mock.process_data = MagicMock(side_effect=lambda data: [d + ' processed' for d in data])

        aggregator = DataAggregator(data_fetcher=fetcher_mock, preprocessor=preprocessor_mock)

        actual = aggregator.get_data_for_seasons([Season.y2015, Season.y2016], Parameters(2))
        expected = ['2015 processed', '2016 processed']

        self.assertEqual(expected, actual)

    def test_get_data_for_team_in_seasons(self):
        fetcher_mock = Mock()
        fetcher_mock.fetch_data_for_team_in_season = MagicMock(
            side_effect=lambda season, team, param: season.value + ' ' + team.value)

        preprocessor_mock = Mock()
        preprocessor_mock.process_data = MagicMock(side_effect=lambda data: [d + ' processed' for d in data])

        aggregator = DataAggregator(data_fetcher=fetcher_mock, preprocessor=preprocessor_mock)

        actual = aggregator.get_data_for_team_in_seasons(Team.Arsenal, [Season.y2015, Season.y2016], Parameters(2))
        expected = ['2015 3459 processed', '2016 3459 processed']

        self.assertEqual(expected, actual)


if __name__ == '__main__':
    unittest.main()
