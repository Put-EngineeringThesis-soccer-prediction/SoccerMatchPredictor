import os
import sys
import unittest
import pandas as pd

sys.path.append(os.path.abspath('..'))
from match import Match
from past_encounter import PastEncounter
from preprocessor import Preprocessor


class TestPreprocessor(unittest.TestCase):

    def test_process_data(self):
        data = [test_match_response]
        match_id = 4389
        preprocessor = Preprocessor()

        actual_matches, past_encounters = preprocessor.process_data(data)
        expected_matches = pd.DataFrame([vars(test_match_object)])
        expected_past_encounters = pd.DataFrame([vars(test_past_encounters)])

        self.assertTrue(expected_matches.equals(actual_matches))
        self.assertTrue(expected_past_encounters.equals(past_encounters[match_id]))


test_match_object = Match(4389, 3459, 3463, 'Arsenal', 'West Ham United', '2015-08-09T00:00:00', 2, 47.38, 52.62,
                          7, 6, 75, 47, 28.27, 26.09, 81.09, 73.64, 1848.527587890, 1594.190063480, 1.3, 5.51, 11.11,
                          6.0, 5.0, 10.5, 2.0, 1, 0, 1, 0, 0, 2, 4, 1)

test_past_encounters = PastEncounter(3459, 3463, "Arsenal", "West Ham United", "2015-03-14T00:00:00", 1)

test_match_response = '[{"MatchDetails":{"Id":4389,"CountryId":1729,"LeagueId":1729,"Season":"2015/2016","Stage":1,' \
                      '"Date":"2015-08-09T00:00:00","MatchApiId":1987032,"HomeTeamApiId":9825,"AwayTeamApiId":8654,' \
                      '"HomeTeamGoal":0,"AwayTeamGoal":2,"HomeTeamShots":22,"AwayTeamShots":8,' \
                      '"HomeTeamShotsOnTarget":6,"AwayTeamShotsOnTarget":4,"HomeTeamCorners":5,"AwayTeamCorners":4,' \
                      '"HomeTeamFoulsCommitted":12,"AwayTeamFoulsCommitted":9,"HomeTeamYellowCards":1,' \
                      '"AwayTeamYellowCards":3,"AwayTeamRedCards":0,"HomeTeamRedCards":0,"B365h":1.29,"B365d":6.00,' \
                      '"B365a":12.00,"Bwh":1.28,"Bwd":5.75,"Bwa":10.50,"Iwh":1.33,"Iwd":4.80,"Iwa":8.30,"Lbh":1.29,' \
                      '"Lbd":5.50,"Lba":12.00,"Psh":1.31,"Psd":5.75,"Psa":12.00,"Whh":1.30,"Whd":5.00,"Wha":11.00,' \
                      '"Sjh":null,"Sjd":null,"Sja":null,"Vch":1.30,"Vcd":5.75,"Vca":12.00,"Gbh":null,"Gbd":null,' \
                      '"Gba":null,"Bsh":null,"Bsd":null,"Bsa":null},"HomeTeam":{"Id":3459,"TeamApiId":9825,' \
                      '"TeamName":"Arsenal","SeasonsPlayed":7,"LastSeasonPoints":75,"Players":[{"Id":15,' \
                      '"FullName":"Aaron Ramsey","Birthday":"1990-12-26T00:00:00","Height":177,"Weight":154,' \
                      '"Attributes":{"Id":275,"PlayerFifaApiId":186561,"PlayerApiId":75489,' \
                      '"Date":"2015-02-06T00:00:00","OverallRating":82,"Potential":86,"PreferredFoot":"right",' \
                      '"AttackingWorkRate":"high","DefensiveWorkRate":"high","Crossing":74,"Finishing":75,' \
                      '"HeadingAccuracy":58,"ShortPassing":84,"Volleys":81,"Dribbling":81,"Curve":64,' \
                      '"FreeKickAccuracy":70,"LongPassing":82,"BallControl":82,"Acceleration":73,"SprintSpeed":69,' \
                      '"Agility":74,"Reactions":81,"Balance":75,"ShotPower":81,"Jumping":63,"Stamina":93,' \
                      '"Strength":68,"LongShots":78,"Aggression":75,"Interceptions":72,"Positioning":84,"Vision":81,' \
                      '"Penalties":76,"Marking":65,"StandingTackle":75,"SlidingTackle":65,"GkDiving":6,' \
                      '"GkHandling":11,"GkKicking":5,"GkPositioning":10,"GkReflexes":8}},{"Id":425,"FullName":"Alex ' \
                      'Oxlade-Chamberlain","Birthday":"1993-08-15T00:00:00","Height":175,"Weight":154,"Attributes":{' \
                      '"Id":7158,"PlayerFifaApiId":198784,"PlayerApiId":196386,"Date":"2015-03-13T00:00:00",' \
                      '"OverallRating":78,"Potential":84,"PreferredFoot":"right","AttackingWorkRate":"high",' \
                      '"DefensiveWorkRate":"medium","Crossing":72,"Finishing":69,"HeadingAccuracy":33,' \
                      '"ShortPassing":76,"Volleys":58,"Dribbling":85,"Curve":66,"FreeKickAccuracy":52,' \
                      '"LongPassing":72,"BallControl":82,"Acceleration":89,"SprintSpeed":89,"Agility":84,' \
                      '"Reactions":74,"Balance":88,"ShotPower":74,"Jumping":65,"Stamina":75,"Strength":68,' \
                      '"LongShots":76,"Aggression":65,"Interceptions":40,"Positioning":73,"Vision":70,"Penalties":68,' \
                      '"Marking":36,"StandingTackle":47,"SlidingTackle":43,"GkDiving":15,"GkHandling":8,' \
                      '"GkKicking":5,"GkPositioning":8,"GkReflexes":8}},{"Id":3484,"FullName":"Francis Coquelin",' \
                      '"Birthday":"1991-05-13T00:00:00","Height":177,"Weight":163,"Attributes":{"Id":58229,' \
                      '"PlayerFifaApiId":189271,"PlayerApiId":159594,"Date":"2015-04-24T00:00:00","OverallRating":75,' \
                      '"Potential":81,"PreferredFoot":"right","AttackingWorkRate":"medium",' \
                      '"DefensiveWorkRate":"high","Crossing":59,"Finishing":44,"HeadingAccuracy":49,' \
                      '"ShortPassing":77,"Volleys":42,"Dribbling":72,"Curve":54,"FreeKickAccuracy":40,' \
                      '"LongPassing":71,"BallControl":77,"Acceleration":71,"SprintSpeed":73,"Agility":76,' \
                      '"Reactions":73,"Balance":82,"ShotPower":70,"Jumping":69,"Stamina":79,"Strength":74,' \
                      '"LongShots":58,"Aggression":84,"Interceptions":78,"Positioning":63,"Vision":72,"Penalties":51,' \
                      '"Marking":71,"StandingTackle":77,"SlidingTackle":74,"GkDiving":8,"GkHandling":8,"GkKicking":7,' \
                      '"GkPositioning":9,"GkReflexes":14}},{"Id":6005,"FullName":"Laurent Koscielny",' \
                      '"Birthday":"1985-09-10T00:00:00","Height":185,"Weight":165,"Attributes":{"Id":99531,' \
                      '"PlayerFifaApiId":165229,"PlayerApiId":46539,"Date":"2014-12-19T00:00:00","OverallRating":82,' \
                      '"Potential":82,"PreferredFoot":"right","AttackingWorkRate":"medium",' \
                      '"DefensiveWorkRate":"high","Crossing":54,"Finishing":32,"HeadingAccuracy":81,' \
                      '"ShortPassing":75,"Volleys":35,"Dribbling":62,"Curve":22,"FreeKickAccuracy":49,' \
                      '"LongPassing":67,"BallControl":67,"Acceleration":79,"SprintSpeed":82,"Agility":70,' \
                      '"Reactions":78,"Balance":62,"ShotPower":54,"Jumping":86,"Stamina":73,"Strength":71,' \
                      '"LongShots":47,"Aggression":86,"Interceptions":83,"Positioning":31,"Vision":56,"Penalties":51,' \
                      '"Marking":81,"StandingTackle":85,"SlidingTackle":83,"GkDiving":13,"GkHandling":11,' \
                      '"GkKicking":9,"GkPositioning":11,"GkReflexes":7}},{"Id":7099,"FullName":"Mathieu Debuchy",' \
                      '"Birthday":"1985-07-28T00:00:00","Height":177,"Weight":168,"Attributes":{"Id":118148,' \
                      '"PlayerFifaApiId":158626,"PlayerApiId":26154,"Date":"2014-12-19T00:00:00","OverallRating":80,' \
                      '"Potential":80,"PreferredFoot":"right","AttackingWorkRate":"high",' \
                      '"DefensiveWorkRate":"medium","Crossing":80,"Finishing":59,"HeadingAccuracy":75,' \
                      '"ShortPassing":77,"Volleys":62,"Dribbling":72,"Curve":74,"FreeKickAccuracy":61,' \
                      '"LongPassing":73,"BallControl":75,"Acceleration":77,"SprintSpeed":75,"Agility":77,' \
                      '"Reactions":76,"Balance":74,"ShotPower":74,"Jumping":80,"Stamina":89,"Strength":70,' \
                      '"LongShots":68,"Aggression":80,"Interceptions":78,"Positioning":67,"Vision":64,"Penalties":66,' \
                      '"Marking":77,"StandingTackle":81,"SlidingTackle":79,"GkDiving":8,"GkHandling":9,"GkKicking":7,' \
                      '"GkPositioning":15,"GkReflexes":10}},{"Id":7327,"FullName":"Mesut Oezil",' \
                      '"Birthday":"1988-10-15T00:00:00","Height":182,"Weight":168,"Attributes":{"Id":122445,' \
                      '"PlayerFifaApiId":176635,"PlayerApiId":36378,"Date":"2015-03-06T00:00:00","OverallRating":86,' \
                      '"Potential":87,"PreferredFoot":"left","AttackingWorkRate":"medium","DefensiveWorkRate":"low",' \
                      '"Crossing":80,"Finishing":74,"HeadingAccuracy":54,"ShortPassing":87,"Volleys":77,' \
                      '"Dribbling":86,"Curve":84,"FreeKickAccuracy":79,"LongPassing":80,"BallControl":90,' \
                      '"Acceleration":77,"SprintSpeed":71,"Agility":84,"Reactions":87,"Balance":76,"ShotPower":70,' \
                      '"Jumping":53,"Stamina":64,"Strength":55,"LongShots":75,"Aggression":54,"Interceptions":24,' \
                      '"Positioning":81,"Vision":92,"Penalties":76,"Marking":22,"StandingTackle":25,' \
                      '"SlidingTackle":25,"GkDiving":6,"GkHandling":14,"GkKicking":10,"GkPositioning":6,' \
                      '"GkReflexes":14}},{"Id":7773,"FullName":"Nacho Monreal","Birthday":"1986-02-26T00:00:00",' \
                      '"Height":177,"Weight":159,"Attributes":{"Id":129876,"PlayerFifaApiId":177604,' \
                      '"PlayerApiId":38521,"Date":"2015-04-10T00:00:00","OverallRating":77,"Potential":77,' \
                      '"PreferredFoot":"left","AttackingWorkRate":"medium","DefensiveWorkRate":"high","Crossing":79,' \
                      '"Finishing":38,"HeadingAccuracy":61,"ShortPassing":76,"Volleys":55,"Dribbling":72,"Curve":73,' \
                      '"FreeKickAccuracy":58,"LongPassing":60,"BallControl":76,"Acceleration":79,"SprintSpeed":77,' \
                      '"Agility":75,"Reactions":76,"Balance":67,"ShotPower":70,"Jumping":75,"Stamina":85,' \
                      '"Strength":65,"LongShots":60,"Aggression":71,"Interceptions":79,"Positioning":55,"Vision":65,' \
                      '"Penalties":60,"Marking":77,"StandingTackle":79,"SlidingTackle":77,"GkDiving":6,' \
                      '"GkHandling":4,"GkKicking":8,"GkPositioning":2,"GkReflexes":4}},{"Id":8137,"FullName":"Olivier ' \
                      'Giroud","Birthday":"1986-09-30T00:00:00","Height":193,"Weight":194,"Attributes":{"Id":135955,' \
                      '"PlayerFifaApiId":178509,"PlayerApiId":46469,"Date":"2015-04-17T00:00:00","OverallRating":80,' \
                      '"Potential":80,"PreferredFoot":"left","AttackingWorkRate":"medium",' \
                      '"DefensiveWorkRate":"medium","Crossing":63,"Finishing":80,"HeadingAccuracy":85,' \
                      '"ShortPassing":76,"Volleys":80,"Dribbling":68,"Curve":70,"FreeKickAccuracy":65,' \
                      '"LongPassing":56,"BallControl":76,"Acceleration":61,"SprintSpeed":63,"Agility":58,' \
                      '"Reactions":81,"Balance":45,"ShotPower":84,"Jumping":76,"Stamina":79,"Strength":88,' \
                      '"LongShots":75,"Aggression":76,"Interceptions":41,"Positioning":84,"Vision":75,"Penalties":81,' \
                      '"Marking":26,"StandingTackle":37,"SlidingTackle":20,"GkDiving":12,"GkHandling":15,' \
                      '"GkKicking":11,"GkPositioning":6,"GkReflexes":5}},{"Id":8546,"FullName":"Per Mertesacker",' \
                      '"Birthday":"1984-09-29T00:00:00","Height":198,"Weight":198,"Attributes":{"Id":142352,' \
                      '"PlayerFifaApiId":53612,"PlayerApiId":35606,"Date":"2015-03-27T00:00:00","OverallRating":82,' \
                      '"Potential":82,"PreferredFoot":"right","AttackingWorkRate":"medium",' \
                      '"DefensiveWorkRate":"medium","Crossing":38,"Finishing":36,"HeadingAccuracy":88,' \
                      '"ShortPassing":71,"Volleys":30,"Dribbling":39,"Curve":39,"FreeKickAccuracy":45,' \
                      '"LongPassing":58,"BallControl":65,"Acceleration":29,"SprintSpeed":33,"Agility":29,' \
                      '"Reactions":84,"Balance":28,"ShotPower":71,"Jumping":33,"Stamina":64,"Strength":88,' \
                      '"LongShots":25,"Aggression":69,"Interceptions":86,"Positioning":43,"Vision":58,"Penalties":42,' \
                      '"Marking":87,"StandingTackle":87,"SlidingTackle":84,"GkDiving":12,"GkHandling":13,' \
                      '"GkKicking":5,"GkPositioning":12,"GkReflexes":8}},{"Id":8580,"FullName":"Petr Cech",' \
                      '"Birthday":"1982-05-20T00:00:00","Height":195,"Weight":198,"Attributes":{"Id":142871,' \
                      '"PlayerFifaApiId":48940,"PlayerApiId":30859,"Date":"2014-09-18T00:00:00","OverallRating":85,' \
                      '"Potential":85,"PreferredFoot":"left","AttackingWorkRate":"medium",' \
                      '"DefensiveWorkRate":"medium","Crossing":25,"Finishing":25,"HeadingAccuracy":25,' \
                      '"ShortPassing":35,"Volleys":25,"Dribbling":25,"Curve":25,"FreeKickAccuracy":25,' \
                      '"LongPassing":33,"BallControl":32,"Acceleration":45,"SprintSpeed":52,"Agility":49,' \
                      '"Reactions":86,"Balance":50,"ShotPower":21,"Jumping":38,"Stamina":32,"Strength":65,' \
                      '"LongShots":25,"Aggression":25,"Interceptions":23,"Positioning":25,"Vision":25,"Penalties":43,' \
                      '"Marking":25,"StandingTackle":25,"SlidingTackle":25,"GkDiving":83,"GkHandling":84,' \
                      '"GkKicking":73,"GkPositioning":83,"GkReflexes":84}},{"Id":9492,"FullName":"Santi Cazorla",' \
                      '"Birthday":"1984-12-13T00:00:00","Height":167,"Weight":146,"Attributes":{"Id":157742,' \
                      '"PlayerFifaApiId":146562,"PlayerApiId":37436,"Date":"2015-04-10T00:00:00","OverallRating":85,' \
                      '"Potential":85,"PreferredFoot":"right","AttackingWorkRate":"medium",' \
                      '"DefensiveWorkRate":"medium","Crossing":83,"Finishing":77,"HeadingAccuracy":53,' \
                      '"ShortPassing":86,"Volleys":67,"Dribbling":87,"Curve":86,"FreeKickAccuracy":78,' \
                      '"LongPassing":84,"BallControl":86,"Acceleration":79,"SprintSpeed":68,"Agility":86,' \
                      '"Reactions":85,"Balance":91,"ShotPower":78,"Jumping":71,"Stamina":65,"Strength":60,' \
                      '"LongShots":82,"Aggression":57,"Interceptions":55,"Positioning":82,"Vision":85,"Penalties":80,' \
                      '"Marking":54,"StandingTackle":62,"SlidingTackle":48,"GkDiving":6,"GkHandling":9,"GkKicking":5,' \
                      '"GkPositioning":7,"GkReflexes":15}}],"Attributes":{"Id":75,"TeamFifaApiId":1,"TeamApiId":9825,' \
                      '"Date":"2014-09-19T00:00:00","BuildUpPlaySpeed":59,"BuildUpPlaySpeedClass":"Balanced",' \
                      '"BuildUpPlayDribbling":51,"BuildUpPlayDribblingClass":"Normal","BuildUpPlayPassing":26,' \
                      '"BuildUpPlayPassingClass":"Short","BuildUpPlayPositioningClass":"Organised",' \
                      '"ChanceCreationPassing":28,"ChanceCreationPassingClass":"Safe","ChanceCreationCrossing":55,' \
                      '"ChanceCreationCrossingClass":"Normal","ChanceCreationShooting":64,' \
                      '"ChanceCreationShootingClass":"Normal","ChanceCreationPositioningClass":"Free Form",' \
                      '"DefencePressure":51,"DefencePressureClass":"Medium","DefenceAggression":44,' \
                      '"DefenceAggressionClass":"Press","DefenceTeamWidth":52,"DefenceTeamWidthClass":"Normal",' \
                      '"DefenceDefenderLineClass":"Cover"},"CurrentEloRating":{"Id":2664,"Rank":"11",' \
                      '"TeamApiId":9825,"CountryId":1729,"Level":1,"Elo":1848.527587890,' \
                      '"StartDate":"2015-08-07T00:00:00","EndDate":"2015-08-09T00:00:00","Country":null}},' \
                      '"AwayTeam":{"Id":3463,"TeamApiId":8654,"TeamName":"West Ham United","SeasonsPlayed":6,' \
                      '"LastSeasonPoints":47,"Players":[{"Id":2,"FullName":"Aaron Cresswell",' \
                      '"Birthday":"1989-12-15T00:00:00","Height":170,"Weight":146,"Attributes":{"Id":14,' \
                      '"PlayerFifaApiId":189615,"PlayerApiId":155782,"Date":"2015-01-09T00:00:00","OverallRating":71,' \
                      '"Potential":75,"PreferredFoot":"left","AttackingWorkRate":"medium",' \
                      '"DefensiveWorkRate":"medium","Crossing":78,"Finishing":50,"HeadingAccuracy":56,' \
                      '"ShortPassing":69,"Volleys":28,"Dribbling":66,"Curve":67,"FreeKickAccuracy":68,' \
                      '"LongPassing":67,"BallControl":67,"Acceleration":79,"SprintSpeed":82,"Agility":79,' \
                      '"Reactions":71,"Balance":90,"ShotPower":70,"Jumping":84,"Stamina":79,"Strength":50,' \
                      '"LongShots":56,"Aggression":66,"Interceptions":65,"Positioning":57,"Vision":64,"Penalties":58,' \
                      '"Marking":73,"StandingTackle":72,"SlidingTackle":72,"GkDiving":13,"GkHandling":6,' \
                      '"GkKicking":8,"GkPositioning":8,"GkReflexes":11}},{"Id":184,"FullName":"Adrian",' \
                      '"Birthday":"1987-01-31T00:00:00","Height":187,"Weight":170,"Attributes":{"Id":3146,' \
                      '"PlayerFifaApiId":194911,"PlayerApiId":169756,"Date":"2015-01-16T00:00:00","OverallRating":77,' \
                      '"Potential":77,"PreferredFoot":"right","AttackingWorkRate":"medium",' \
                      '"DefensiveWorkRate":"medium","Crossing":25,"Finishing":25,"HeadingAccuracy":25,' \
                      '"ShortPassing":20,"Volleys":25,"Dribbling":25,"Curve":25,"FreeKickAccuracy":25,' \
                      '"LongPassing":31,"BallControl":22,"Acceleration":46,"SprintSpeed":44,"Agility":47,' \
                      '"Reactions":70,"Balance":48,"ShotPower":39,"Jumping":69,"Stamina":38,"Strength":74,' \
                      '"LongShots":25,"Aggression":37,"Interceptions":24,"Positioning":25,"Vision":25,"Penalties":41,' \
                      '"Marking":25,"StandingTackle":25,"SlidingTackle":25,"GkDiving":77,"GkHandling":75,' \
                      '"GkKicking":70,"GkPositioning":75,"GkReflexes":84}},{"Id":808,"FullName":"Angelo Obinze ' \
                      'Ogbonna","Birthday":"1988-05-23T00:00:00","Height":190,"Weight":190,"Attributes":{"Id":13946,' \
                      '"PlayerFifaApiId":183855,"PlayerApiId":49543,"Date":"2015-02-27T00:00:00","OverallRating":77,' \
                      '"Potential":80,"PreferredFoot":"left","AttackingWorkRate":"medium","DefensiveWorkRate":"high",' \
                      '"Crossing":50,"Finishing":24,"HeadingAccuracy":76,"ShortPassing":55,"Volleys":29,' \
                      '"Dribbling":48,"Curve":50,"FreeKickAccuracy":35,"LongPassing":61,"BallControl":59,' \
                      '"Acceleration":69,"SprintSpeed":77,"Agility":58,"Reactions":65,"Balance":54,"ShotPower":71,' \
                      '"Jumping":87,"Stamina":69,"Strength":87,"LongShots":50,"Aggression":65,"Interceptions":83,' \
                      '"Positioning":32,"Vision":27,"Penalties":38,"Marking":84,"StandingTackle":84,' \
                      '"SlidingTackle":78,"GkDiving":4,"GkHandling":2,"GkKicking":4,"GkPositioning":3,' \
                      '"GkReflexes":3}},{"Id":1690,"FullName":"Cheikhou Kouyate","Birthday":"1989-12-21T00:00:00",' \
                      '"Height":193,"Weight":172,"Attributes":{"Id":28071,"PlayerFifaApiId":186801,' \
                      '"PlayerApiId":148302,"Date":"2015-04-10T00:00:00","OverallRating":72,"Potential":78,' \
                      '"PreferredFoot":"right","AttackingWorkRate":"medium","DefensiveWorkRate":"medium",' \
                      '"Crossing":46,"Finishing":54,"HeadingAccuracy":78,"ShortPassing":66,"Volleys":46,' \
                      '"Dribbling":61,"Curve":38,"FreeKickAccuracy":46,"LongPassing":64,"BallControl":68,' \
                      '"Acceleration":67,"SprintSpeed":87,"Agility":59,"Reactions":74,"Balance":57,"ShotPower":71,' \
                      '"Jumping":78,"Stamina":92,"Strength":86,"LongShots":58,"Aggression":79,"Interceptions":71,' \
                      '"Positioning":58,"Vision":60,"Penalties":51,"Marking":67,"StandingTackle":78,' \
                      '"SlidingTackle":73,"GkDiving":14,"GkHandling":7,"GkKicking":8,"GkPositioning":11,' \
                      '"GkReflexes":8}},{"Id":2592,"FullName":"Diafra Sakho","Birthday":"1989-12-24T00:00:00",' \
                      '"Height":182,"Weight":174,"Attributes":{"Id":43267,"PlayerFifaApiId":193554,' \
                      '"PlayerApiId":192899,"Date":"2015-02-27T00:00:00","OverallRating":72,"Potential":78,' \
                      '"PreferredFoot":"right","AttackingWorkRate":"medium","DefensiveWorkRate":"medium",' \
                      '"Crossing":52,"Finishing":78,"HeadingAccuracy":75,"ShortPassing":53,"Volleys":67,' \
                      '"Dribbling":69,"Curve":52,"FreeKickAccuracy":61,"LongPassing":44,"BallControl":65,' \
                      '"Acceleration":76,"SprintSpeed":78,"Agility":73,"Reactions":63,"Balance":57,"ShotPower":67,' \
                      '"Jumping":71,"Stamina":62,"Strength":71,"LongShots":62,"Aggression":61,"Interceptions":36,' \
                      '"Positioning":78,"Vision":55,"Penalties":66,"Marking":23,"StandingTackle":25,' \
                      '"SlidingTackle":21,"GkDiving":11,"GkHandling":9,"GkKicking":8,"GkPositioning":12,' \
                      '"GkReflexes":15}},{"Id":2671,"FullName":"Dimitri Payet","Birthday":"1987-03-29T00:00:00",' \
                      '"Height":175,"Weight":170,"Attributes":{"Id":44740,"PlayerFifaApiId":177388,' \
                      '"PlayerApiId":25496,"Date":"2015-05-08T00:00:00","OverallRating":81,"Potential":82,' \
                      '"PreferredFoot":"right","AttackingWorkRate":"high","DefensiveWorkRate":"medium","Crossing":83,' \
                      '"Finishing":73,"HeadingAccuracy":59,"ShortPassing":82,"Volleys":79,"Dribbling":83,"Curve":81,' \
                      '"FreeKickAccuracy":79,"LongPassing":84,"BallControl":81,"Acceleration":79,"SprintSpeed":77,' \
                      '"Agility":79,"Reactions":76,"Balance":76,"ShotPower":77,"Jumping":48,"Stamina":71,' \
                      '"Strength":68,"LongShots":83,"Aggression":54,"Interceptions":44,"Positioning":77,"Vision":83,' \
                      '"Penalties":76,"Marking":29,"StandingTackle":51,"SlidingTackle":48,"GkDiving":11,' \
                      '"GkHandling":15,"GkKicking":9,"GkPositioning":14,"GkReflexes":12}},{"Id":4600,' \
                      '"FullName":"James Tomkins","Birthday":"1989-03-29T00:00:00","Height":190,"Weight":163,' \
                      '"Attributes":{"Id":76458,"PlayerFifaApiId":173546,"PlayerApiId":109897,' \
                      '"Date":"2015-02-13T00:00:00","OverallRating":73,"Potential":77,"PreferredFoot":"right",' \
                      '"AttackingWorkRate":"medium","DefensiveWorkRate":"medium","Crossing":41,"Finishing":28,' \
                      '"HeadingAccuracy":79,"ShortPassing":58,"Volleys":27,"Dribbling":43,"Curve":32,' \
                      '"FreeKickAccuracy":30,"LongPassing":58,"BallControl":62,"Acceleration":64,"SprintSpeed":64,' \
                      '"Agility":46,"Reactions":72,"Balance":50,"ShotPower":39,"Jumping":71,"Stamina":75,' \
                      '"Strength":76,"LongShots":28,"Aggression":66,"Interceptions":73,"Positioning":44,"Vision":49,' \
                      '"Penalties":42,"Marking":74,"StandingTackle":77,"SlidingTackle":76,"GkDiving":10,' \
                      '"GkHandling":7,"GkKicking":5,"GkPositioning":8,"GkReflexes":13}},{"Id":6881,"FullName":"Mark ' \
                      'Noble","Birthday":"1987-05-08T00:00:00","Height":180,"Weight":168,"Attributes":{"Id":114689,' \
                      '"PlayerFifaApiId":152879,"PlayerApiId":37169,"Date":"2015-02-27T00:00:00","OverallRating":75,' \
                      '"Potential":75,"PreferredFoot":"right","AttackingWorkRate":"high","DefensiveWorkRate":"high",' \
                      '"Crossing":70,"Finishing":59,"HeadingAccuracy":59,"ShortPassing":79,"Volleys":67,' \
                      '"Dribbling":69,"Curve":70,"FreeKickAccuracy":68,"LongPassing":76,"BallControl":76,' \
                      '"Acceleration":54,"SprintSpeed":53,"Agility":63,"Reactions":72,"Balance":72,"ShotPower":73,' \
                      '"Jumping":59,"Stamina":90,"Strength":66,"LongShots":62,"Aggression":80,"Interceptions":75,' \
                      '"Positioning":63,"Vision":77,"Penalties":87,"Marking":62,"StandingTackle":74,' \
                      '"SlidingTackle":75,"GkDiving":6,"GkHandling":5,"GkKicking":13,"GkPositioning":15,' \
                      '"GkReflexes":10}},{"Id":7245,"FullName":"Mauro Zarate","Birthday":"1987-03-18T00:00:00",' \
                      '"Height":175,"Weight":168,"Attributes":{"Id":120957,"PlayerFifaApiId":153177,' \
                      '"PlayerApiId":18506,"Date":"2015-06-05T00:00:00","OverallRating":76,"Potential":76,' \
                      '"PreferredFoot":"right","AttackingWorkRate":"high","DefensiveWorkRate":"low","Crossing":67,' \
                      '"Finishing":79,"HeadingAccuracy":58,"ShortPassing":71,"Volleys":77,"Dribbling":83,"Curve":85,' \
                      '"FreeKickAccuracy":78,"LongPassing":60,"BallControl":81,"Acceleration":77,"SprintSpeed":73,' \
                      '"Agility":88,"Reactions":78,"Balance":88,"ShotPower":79,"Jumping":69,"Stamina":70,' \
                      '"Strength":61,"LongShots":79,"Aggression":65,"Interceptions":38,"Positioning":78,"Vision":70,' \
                      '"Penalties":73,"Marking":31,"StandingTackle":36,"SlidingTackle":32,"GkDiving":6,' \
                      '"GkHandling":15,"GkKicking":15,"GkPositioning":5,"GkReflexes":11}},{"Id":8840,' \
                      '"FullName":"Reece Oxford","Birthday":"1998-12-16T00:00:00","Height":190,"Weight":157,' \
                      '"Attributes":{"Id":147408,"PlayerFifaApiId":225908,"PlayerApiId":575789,' \
                      '"Date":"2007-02-22T00:00:00","OverallRating":61,"Potential":83,"PreferredFoot":"right",' \
                      '"AttackingWorkRate":"medium","DefensiveWorkRate":"medium","Crossing":34,"Finishing":22,' \
                      '"HeadingAccuracy":64,"ShortPassing":63,"Volleys":36,"Dribbling":41,"Curve":35,' \
                      '"FreeKickAccuracy":36,"LongPassing":49,"BallControl":62,"Acceleration":68,"SprintSpeed":74,' \
                      '"Agility":51,"Reactions":62,"Balance":53,"ShotPower":49,"Jumping":66,"Stamina":65,' \
                      '"Strength":66,"LongShots":37,"Aggression":55,"Interceptions":62,"Positioning":34,"Vision":53,' \
                      '"Penalties":48,"Marking":65,"StandingTackle":67,"SlidingTackle":65,"GkDiving":10,' \
                      '"GkHandling":11,"GkKicking":7,"GkPositioning":9,"GkReflexes":14}},{"Id":10827,' \
                      '"FullName":"Winston Reid","Birthday":"1988-07-03T00:00:00","Height":190,"Weight":192,' \
                      '"Attributes":{"Id":180224,"PlayerFifaApiId":176285,"PlayerApiId":35110,' \
                      '"Date":"2015-03-06T00:00:00","OverallRating":75,"Potential":78,"PreferredFoot":"right",' \
                      '"AttackingWorkRate":"medium","DefensiveWorkRate":"high","Crossing":27,"Finishing":45,' \
                      '"HeadingAccuracy":82,"ShortPassing":63,"Volleys":29,"Dribbling":42,"Curve":32,' \
                      '"FreeKickAccuracy":37,"LongPassing":62,"BallControl":56,"Acceleration":68,"SprintSpeed":79,' \
                      '"Agility":54,"Reactions":70,"Balance":43,"ShotPower":74,"Jumping":70,"Stamina":76,' \
                      '"Strength":81,"LongShots":52,"Aggression":73,"Interceptions":74,"Positioning":41,"Vision":43,' \
                      '"Penalties":54,"Marking":76,"StandingTackle":80,"SlidingTackle":77,"GkDiving":12,' \
                      '"GkHandling":14,"GkKicking":8,"GkPositioning":9,"GkReflexes":11}}],"Attributes":{"Id":1389,' \
                      '"TeamFifaApiId":19,"TeamApiId":8654,"Date":"2014-09-19T00:00:00","BuildUpPlaySpeed":72,' \
                      '"BuildUpPlaySpeedClass":"Fast","BuildUpPlayDribbling":39,"BuildUpPlayDribblingClass":"Normal",' \
                      '"BuildUpPlayPassing":73,"BuildUpPlayPassingClass":"Long",' \
                      '"BuildUpPlayPositioningClass":"Organised","ChanceCreationPassing":68,' \
                      '"ChanceCreationPassingClass":"Risky","ChanceCreationCrossing":71,' \
                      '"ChanceCreationCrossingClass":"Lots","ChanceCreationShooting":32,' \
                      '"ChanceCreationShootingClass":"Little","ChanceCreationPositioningClass":"Organised",' \
                      '"DefencePressure":30,"DefencePressureClass":"Deep","DefenceAggression":34,' \
                      '"DefenceAggressionClass":"Press","DefenceTeamWidth":41,"DefenceTeamWidthClass":"Normal",' \
                      '"DefenceDefenderLineClass":"Cover"},"CurrentEloRating":{"Id":7165,"Rank":"None",' \
                      '"TeamApiId":8654,"CountryId":1729,"Level":1,"Elo":1594.190063480,' \
                      '"StartDate":"2015-08-07T00:00:00","EndDate":"2015-08-09T00:00:00","Country":null}},' \
                      '"HomeTeamHistory":{"MatchHistory":[{"Id":4319,"CountryId":1729,"LeagueId":1729,' \
                      '"Season":"2014/2015","Stage":38,"Date":"2015-05-24T00:00:00","MatchApiId":1724352,' \
                      '"HomeTeamApiId":9825,"AwayTeamApiId":8659,"HomeTeamGoal":4,"AwayTeamGoal":1,' \
                      '"HomeTeamShots":24,"AwayTeamShots":14,"HomeTeamShotsOnTarget":13,"AwayTeamShotsOnTarget":5,' \
                      '"HomeTeamCorners":7,"AwayTeamCorners":3,"HomeTeamFoulsCommitted":6,"AwayTeamFoulsCommitted":7,' \
                      '"HomeTeamYellowCards":1,"AwayTeamYellowCards":0,"AwayTeamRedCards":0,"HomeTeamRedCards":0,' \
                      '"B365h":1.40,"B365d":5.25,"B365a":8.50,"Bwh":1.40,"Bwd":4.75,"Bwa":7.00,"Iwh":1.40,"Iwd":4.40,' \
                      '"Iwa":7.30,"Lbh":1.36,"Lbd":5.00,"Lba":9.00,"Psh":1.43,"Psd":4.91,"Psa":8.53,"Whh":1.40,' \
                      '"Whd":4.50,"Wha":8.00,"Sjh":null,"Sjd":null,"Sja":null,"Vch":1.40,"Vcd":5.00,"Vca":9.00,' \
                      '"Gbh":null,"Gbd":null,"Gba":null,"Bsh":null,"Bsd":null,"Bsa":null},{"Id":4269,' \
                      '"CountryId":1729,"LeagueId":1729,"Season":"2014/2015","Stage":33,"Date":"2015-05-20T00:00:00",' \
                      '"MatchApiId":1724302,"HomeTeamApiId":9825,"AwayTeamApiId":8472,"HomeTeamGoal":0,' \
                      '"AwayTeamGoal":0,"HomeTeamShots":28,"AwayTeamShots":7,"HomeTeamShotsOnTarget":8,' \
                      '"AwayTeamShotsOnTarget":3,"HomeTeamCorners":5,"AwayTeamCorners":2,"HomeTeamFoulsCommitted":6,' \
                      '"AwayTeamFoulsCommitted":8,"HomeTeamYellowCards":1,"AwayTeamYellowCards":0,' \
                      '"AwayTeamRedCards":0,"HomeTeamRedCards":0,"B365h":1.30,"B365d":6.00,"B365a":11.00,"Bwh":1.30,' \
                      '"Bwd":5.75,"Bwa":10.00,"Iwh":1.35,"Iwd":4.60,"Iwa":8.20,"Lbh":1.30,"Lbd":5.50,"Lba":11.00,' \
                      '"Psh":1.32,"Psd":5.46,"Psa":12.31,"Whh":1.29,"Whd":5.50,"Wha":11.00,"Sjh":null,"Sjd":null,' \
                      '"Sja":null,"Vch":1.29,"Vcd":6.00,"Vca":13.00,"Gbh":null,"Gbd":null,"Gba":null,"Bsh":null,' \
                      '"Bsd":null,"Bsa":null}]},"AwayTeamHistory":{"MatchHistory":[{"Id":4327,"CountryId":1729,' \
                      '"LeagueId":1729,"Season":"2014/2015","Stage":38,"Date":"2015-05-24T00:00:00",' \
                      '"MatchApiId":1724360,"HomeTeamApiId":10261,"AwayTeamApiId":8654,"HomeTeamGoal":2,' \
                      '"AwayTeamGoal":0,"HomeTeamShots":17,"AwayTeamShots":4,"HomeTeamShotsOnTarget":4,' \
                      '"AwayTeamShotsOnTarget":1,"HomeTeamCorners":2,"AwayTeamCorners":3,"HomeTeamFoulsCommitted":9,' \
                      '"AwayTeamFoulsCommitted":9,"HomeTeamYellowCards":2,"AwayTeamYellowCards":1,' \
                      '"AwayTeamRedCards":0,"HomeTeamRedCards":0,"B365h":1.83,"B365d":4.00,"B365a":4.33,"Bwh":1.80,' \
                      '"Bwd":3.90,"Bwa":4.40,"Iwh":1.65,"Iwd":3.80,"Iwa":4.60,"Lbh":1.80,"Lbd":3.75,"Lba":4.50,' \
                      '"Psh":1.79,"Psd":4.00,"Psa":4.69,"Whh":1.80,"Whd":3.50,"Wha":4.50,"Sjh":null,"Sjd":null,' \
                      '"Sja":null,"Vch":1.80,"Vcd":4.00,"Vca":4.60,"Gbh":null,"Gbd":null,"Gba":null,"Bsh":null,' \
                      '"Bsd":null,"Bsa":null},{"Id":4318,"CountryId":1729,"LeagueId":1729,"Season":"2014/2015",' \
                      '"Stage":37,"Date":"2015-05-16T00:00:00","MatchApiId":1724351,"HomeTeamApiId":8654,' \
                      '"AwayTeamApiId":8668,"HomeTeamGoal":1,"AwayTeamGoal":2,"HomeTeamShots":13,"AwayTeamShots":21,' \
                      '"HomeTeamShotsOnTarget":3,"AwayTeamShotsOnTarget":5,"HomeTeamCorners":7,"AwayTeamCorners":8,' \
                      '"HomeTeamFoulsCommitted":12,"AwayTeamFoulsCommitted":14,"HomeTeamYellowCards":1,' \
                      '"AwayTeamYellowCards":4,"AwayTeamRedCards":0,"HomeTeamRedCards":0,"B365h":2.90,"B365d":3.40,' \
                      '"B365a":2.60,"Bwh":2.65,"Bwd":3.20,"Bwa":2.60,"Iwh":2.60,"Iwd":3.20,"Iwa":2.60,"Lbh":2.80,' \
                      '"Lbd":3.40,"Lba":2.62,"Psh":2.99,"Psd":3.50,"Psa":2.51,"Whh":2.80,"Whd":3.10,"Wha":2.60,' \
                      '"Sjh":null,"Sjd":null,"Sja":null,"Vch":3.00,"Vcd":3.40,"Vca":2.50,"Gbh":null,"Gbd":null,' \
                      '"Gba":null,"Bsh":null,"Bsd":null,"Bsa":null}]},"PastEncounters":[{"HomeTeamId":3459,' \
                      '"HomeTeamApiId":9825,"HomeTeamName":"Arsenal","AwayTeamId":3463,"AwayTeamApiId":8654,' \
                      '"AwayTeamName":"West Ham United","Date":"2015-03-14T00:00:00","HomeTeamGoals":3,' \
                      '"AwayTeamGoals":0}]}] '
