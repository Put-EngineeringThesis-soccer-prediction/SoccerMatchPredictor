from data_aggregator import DataAggregator
from season import Season
from parameters import Parameters
from team import Team

if __name__ == '__main__':
    data_aggregator = DataAggregator()
    data_aggregator.get_data_for_seasons([Season.y2011, Season.y2012], Parameters(no_last_matches=3))
    data_aggregator.get_data_for_team_in_seasons(Team.Arsenal, [Season.y2011, Season.y2012],
                                                 Parameters(no_last_matches=3))
