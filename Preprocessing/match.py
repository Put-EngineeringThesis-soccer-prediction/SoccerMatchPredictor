class Match:

    def __init__(self, match_id, home_team_id, away_team_id, home_team_name, away_team_name, match_date, match_result,
                 home_team_score, away_team_score, home_team_seasons_played, away_team_seasons_played,
                 home_team_last_season_points, away_team_last_season_points, home_players_avg_age, away_players_avg_age,
                 home_players_avg_rating, away_players_avg_rating, home_elo_rating, away_elo_rating, avg_home_win_odds,
                 avg_draw_odds, avg_away_win_odds, home_avg_corners, away_avg_corners, home_avg_shots, away_avg_shots,
                 home_won_games, away_won_games, home_tied_games, away_tied_games, home_lost_games, away_lost_games,
                 home_scored_goals, away_scored_goals):
        self.match_id = match_id
        self.home_team_id = home_team_id
        self.away_team_id = away_team_id
        self.home_team_name = home_team_name
        self.away_team_name = away_team_name
        self.match_date = match_date
        self.match_result = match_result
        self.home_team_score = home_team_score
        self.away_team_score = away_team_score
        self.home_team_seasons_played = home_team_seasons_played
        self.away_team_seasons_played = away_team_seasons_played
        self.home_team_last_season_points = home_team_last_season_points
        self.away_team_last_season_points = away_team_last_season_points
        self.home_players_avg_age = home_players_avg_age
        self.away_players_avg_age = away_players_avg_age
        self.home_players_avg_rating = home_players_avg_rating
        self.away_players_avg_rating = away_players_avg_rating
        self.home_elo_rating = home_elo_rating
        self.away_elo_rating = away_elo_rating
        self.avg_home_win_odds = avg_home_win_odds
        self.avg_draw_odds = avg_draw_odds
        self.avg_away_win_odds = avg_away_win_odds
        self.home_avg_corners = home_avg_corners
        self.away_avg_corners = away_avg_corners
        self.home_avg_shots = home_avg_shots
        self.away_avg_shots = away_avg_shots
        self.home_won_games = home_won_games
        self.away_won_games = away_won_games
        self.home_tied_games = home_tied_games
        self.away_tied_games = away_tied_games
        self.home_lost_games = home_lost_games
        self.away_lost_games = away_lost_games
        self.home_scored_goals = home_scored_goals
        self.away_scored_goals = away_scored_goals
