import json
from types import SimpleNamespace
from datetime import datetime
from match import Match
from past_encounter import PastEncounter
import pandas as pd


class Preprocessor:
    date_format = '%Y-%m-%dT%H:%M:%S'

    def process_data(self, data):
        matches = []
        past_encounters = {}

        for season in data:
            raw_matches = json.loads(season, object_hook=lambda d: SimpleNamespace(**d))

            for raw in raw_matches:
                # proceed only if history is provided for both teams
                if raw.HomeTeamHistory.MatchHistory and raw.AwayTeamHistory.MatchHistory:
                    match_id = raw.MatchDetails.Id
                    home_team_id = raw.HomeTeam.Id
                    away_team_id = raw.AwayTeam.Id
                    home_team_name = raw.HomeTeam.TeamName
                    away_team_name = raw.AwayTeam.TeamName
                    home_team_api_id = raw.MatchDetails.HomeTeamApiId
                    away_team_api_id = raw.MatchDetails.AwayTeamApiId
                    raw_match_date = raw.MatchDetails.Date
                    match_date = datetime.strptime(raw.MatchDetails.Date, self.date_format)
                    match_result = self.__get_match_result(raw.MatchDetails)

                    home_team_score = self.__calculate_team_score(raw.HomeTeam.Attributes)
                    away_team_score = self.__calculate_team_score(raw.AwayTeam.Attributes)
                    home_team_seasons_played = int(raw.HomeTeam.SeasonsPlayed)
                    away_team_seasons_played = int(raw.AwayTeam.SeasonsPlayed)
                    home_team_last_season_points = int(raw.HomeTeam.LastSeasonPoints)
                    away_team_last_season_points = int(raw.AwayTeam.LastSeasonPoints)
                    home_players_avg_age = self.__calculate_players_age(raw.HomeTeam.Players, match_date)
                    away_players_avg_age = self.__calculate_players_age(raw.AwayTeam.Players, match_date)
                    home_players_avg_rating = self.__calculate_players_rating(raw.HomeTeam.Players)
                    away_players_avg_rating = self.__calculate_players_rating(raw.AwayTeam.Players)
                    home_elo_rating = raw.HomeTeam.CurrentEloRating.Elo
                    away_elo_rating = raw.AwayTeam.CurrentEloRating.Elo
                    avg_home_win_odds = self.__calculate_home_win_odds(raw.MatchDetails)
                    avg_draw_odds = self.__calculate_draw_odds(raw.MatchDetails)
                    avg_away_win_odds = self.__calculate_away_win_odds(raw.MatchDetails)
                    home_avg_corners = self.__calculate_avg_corners(raw.HomeTeamHistory.MatchHistory, home_team_api_id)
                    away_avg_corners = self.__calculate_avg_corners(raw.AwayTeamHistory.MatchHistory, away_team_api_id)
                    home_avg_shots = self.__calculate_avg_shots(raw.HomeTeamHistory.MatchHistory, home_team_api_id)
                    away_avg_shots = self.__calculate_avg_shots(raw.AwayTeamHistory.MatchHistory, away_team_api_id)
                    home_won_games = self.__calculate_won_games(raw.HomeTeamHistory.MatchHistory, home_team_api_id)
                    away_won_games = self.__calculate_won_games(raw.AwayTeamHistory.MatchHistory, away_team_api_id)
                    home_tied_games = self.__calculate_tied_games(raw.HomeTeamHistory.MatchHistory)
                    away_tied_games = self.__calculate_tied_games(raw.AwayTeamHistory.MatchHistory)
                    home_lost_games = self.__calculate_lost_games(raw.HomeTeamHistory.MatchHistory, home_team_api_id)
                    away_lost_games = self.__calculate_lost_games(raw.AwayTeamHistory.MatchHistory, away_team_api_id)
                    home_scored_goals = self.__calculate_scored_goals(raw.HomeTeamHistory.MatchHistory,
                                                                      home_team_api_id)
                    away_scored_goals = self.__calculate_scored_goals(raw.AwayTeamHistory.MatchHistory,
                                                                      away_team_api_id)

                    match = Match(match_id, home_team_id, away_team_id, home_team_name, away_team_name, raw_match_date,
                                  match_result, home_team_score, away_team_score, home_team_seasons_played,
                                  away_team_seasons_played, home_team_last_season_points, away_team_last_season_points,
                                  home_players_avg_age, away_players_avg_age, home_players_avg_rating,
                                  away_players_avg_rating, home_elo_rating, away_elo_rating, avg_home_win_odds,
                                  avg_draw_odds, avg_away_win_odds, home_avg_corners, away_avg_corners, home_avg_shots,
                                  away_avg_shots, home_won_games, away_won_games, home_tied_games, away_tied_games,
                                  home_lost_games, away_lost_games, home_scored_goals, away_scored_goals)
                    matches.append(match)

                    encounters = self.__get_past_encounters(raw.PastEncounters)
                    past_encounters[match_id] = encounters

        return pd.DataFrame([vars(m) for m in matches]), past_encounters

    def __get_match_result(self, details):
        result = 0
        if details.HomeTeamGoal > details.AwayTeamGoal:
            result = 1
        elif details.AwayTeamGoal > details.HomeTeamGoal:
            result = 2
        return result

    def __calculate_team_score(self, attr):
        attributes = [attr.BuildUpPlaySpeed, attr.BuildUpPlayPassing, attr.ChanceCreationPassing,
                      attr.ChanceCreationCrossing, attr.ChanceCreationShooting, attr.DefencePressure,
                      attr.DefenceAggression, attr.DefenceTeamWidth]
        attributes = [attr for attr in attributes if attr]
        return round(sum(attributes) / len(attributes), 2)

    def __calculate_players_age(self, players, current_date):
        sum_age = 0
        for player in players:
            player_birthday = datetime.strptime(player.Birthday, self.date_format)
            sum_age = sum_age + (current_date.year - player_birthday.year)
        return round(sum_age / len(players), 2)

    def __calculate_players_rating(self, players):
        ratings = [int(player.Attributes.OverallRating) for player in players if player.Attributes.OverallRating]
        return round(sum(ratings) / len(ratings), 2)

    def __calculate_home_win_odds(self, details):
        odds = [details.B365h, details.Bwh, details.Iwh, details.Lbh, details.Psh, details.Whh, details.Sjh,
                details.Vch, details.Gbh, details.Bsh]
        odds = [odd for odd in odds if odd]
        return round(sum(odds) / len(odds), 2)

    def __calculate_draw_odds(self, details):
        odds = [details.B365d, details.Bwd, details.Iwd, details.Lbd, details.Psd, details.Whd, details.Sjd,
                details.Vcd, details.Gbd, details.Bsd]
        odds = [odd for odd in odds if odd]
        return round(sum(odds) / len(odds), 2)

    def __calculate_away_win_odds(self, details):
        odds = [details.B365a, details.Bwa, details.Iwa, details.Lba, details.Psa, details.Wha, details.Sja,
                details.Vca, details.Gba, details.Bsa]
        odds = [odd for odd in odds if odd]
        return round(sum(odds) / len(odds), 2)

    def __calculate_avg_corners(self, history, team_id):
        sum_corners = 0
        for match in history:
            if match.HomeTeamApiId == team_id:
                sum_corners = sum_corners + match.HomeTeamCorners
            else:
                sum_corners = sum_corners + match.AwayTeamCorners

        return round(sum_corners / len(history), 2)

    def __calculate_avg_shots(self, history, team_id):
        sum_shots = 0
        for match in history:
            if match.HomeTeamApiId == team_id:
                sum_shots = sum_shots + match.HomeTeamShotsOnTarget
            else:
                sum_shots = sum_shots + match.AwayTeamShotsOnTarget

        return round(sum_shots / len(history), 2)

    def __calculate_won_games(self, history, team_id):
        won_games = 0
        for match in history:
            if match.HomeTeamApiId == team_id and match.HomeTeamGoal > match.AwayTeamGoal:
                won_games = won_games + 1
            elif match.AwayTeamApiId == team_id and match.AwayTeamGoal > match.HomeTeamGoal:
                won_games = won_games + 1

        return won_games

    def __calculate_tied_games(self, history):
        tied_games = 0
        for match in history:
            if match.HomeTeamGoal == match.AwayTeamGoal:
                tied_games = tied_games + 1
        return tied_games

    def __calculate_lost_games(self, history, team_id):
        lost_games = 0
        for match in history:
            if match.HomeTeamApiId == team_id and match.HomeTeamGoal < match.AwayTeamGoal:
                lost_games = lost_games + 1
            elif match.AwayTeamApiId == team_id and match.AwayTeamGoal < match.HomeTeamGoal:
                lost_games = lost_games + 1

        return lost_games

    def __calculate_scored_goals(self, history, team_id):
        scored_goals = 0
        for match in history:
            if match.HomeTeamApiId == team_id:
                scored_goals = scored_goals + match.HomeTeamGoal
            elif match.AwayTeamApiId == team_id:
                scored_goals = scored_goals + match.AwayTeamGoal

        return scored_goals

    def __get_past_encounters(self, matches):
        encounters = []
        if matches:
            for match in matches:
                result = 0
                if match.HomeTeamGoals > match.AwayTeamGoals:
                    result = 1
                elif match.AwayTeamGoals > match.HomeTeamGoals:
                    result = 2

                encounters.append(PastEncounter(home_team_id=match.HomeTeamId, away_team_id=match.AwayTeamId,
                                                home_team_name=match.HomeTeamName, away_team_name=match.AwayTeamName,
                                                match_date=match.Date, match_result=result))
        else:
            encounters.append(PastEncounter())

        return pd.DataFrame([vars(e) for e in encounters])
