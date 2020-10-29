using Microsoft.EntityFrameworkCore;

namespace SoccerDataImporter.DatabaseModels
{
    public partial class MatchPredictDbContext : DbContext
    {
        public MatchPredictDbContext()
        {
        }

        public MatchPredictDbContext(DbContextOptions<MatchPredictDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<EloRating> EloRating { get; set; }
        public virtual DbSet<League> League { get; set; }
        public virtual DbSet<Match> Match { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<PlayerAttributes> PlayerAttributes { get; set; }
        public virtual DbSet<PremierLeaguePlayersAttributes> PremierLeaguePlayersAttributes { get; set; }
        public virtual DbSet<PremierLeaguePlayersWithAway> PremierLeaguePlayersWithAway { get; set; }
        public virtual DbSet<PremierLeagueTeams> PremierLeagueTeams { get; set; }
        public virtual DbSet<PremierLeagueTeamsAttributes> PremierLeagueTeamsAttributes { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<TeamAttributes> TeamAttributes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Country__72E12F1BE18EAF18")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EloRating>(entity =>
            {
                entity.Property(e => e.Elo).HasColumnType("numeric(18, 9)");

                entity.Property(e => e.Rank)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.TeamApiId).HasColumnName("team_api_id");

                entity.HasOne(d => d.TeamApi)
                    .WithMany(p => p.EloRating)
                    .HasPrincipalKey(p => p.TeamApiId)
                    .HasForeignKey(d => d.TeamApiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EloRating_Team");

				entity.HasOne(d => d.Country)
					.WithMany(p => p.EloRating)
					.HasPrincipalKey(p => p.Id)
					.HasForeignKey(d => d.CountryId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_EloRating_Country");

			});

            modelBuilder.Entity<League>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__League__72E12F1B61C1AE8C")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.League)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__League__country___3B75D760");
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.HasIndex(e => e.MatchApiId)
                    .HasName("UQ__Match__8EB91268054D5C1F")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AwayPlayer1).HasColumnName("away_player_1");

                entity.Property(e => e.AwayPlayer10).HasColumnName("away_player_10");

                entity.Property(e => e.AwayPlayer11).HasColumnName("away_player_11");

                entity.Property(e => e.AwayPlayer2).HasColumnName("away_player_2");

                entity.Property(e => e.AwayPlayer3).HasColumnName("away_player_3");

                entity.Property(e => e.AwayPlayer4).HasColumnName("away_player_4");

                entity.Property(e => e.AwayPlayer5).HasColumnName("away_player_5");

                entity.Property(e => e.AwayPlayer6).HasColumnName("away_player_6");

                entity.Property(e => e.AwayPlayer7).HasColumnName("away_player_7");

                entity.Property(e => e.AwayPlayer8).HasColumnName("away_player_8");

                entity.Property(e => e.AwayPlayer9).HasColumnName("away_player_9");

                entity.Property(e => e.AwayPlayerX1).HasColumnName("away_player_X1");

                entity.Property(e => e.AwayPlayerX10).HasColumnName("away_player_X10");

                entity.Property(e => e.AwayPlayerX11).HasColumnName("away_player_X11");

                entity.Property(e => e.AwayPlayerX2).HasColumnName("away_player_X2");

                entity.Property(e => e.AwayPlayerX3).HasColumnName("away_player_X3");

                entity.Property(e => e.AwayPlayerX4).HasColumnName("away_player_X4");

                entity.Property(e => e.AwayPlayerX5).HasColumnName("away_player_X5");

                entity.Property(e => e.AwayPlayerX6).HasColumnName("away_player_X6");

                entity.Property(e => e.AwayPlayerX7).HasColumnName("away_player_X7");

                entity.Property(e => e.AwayPlayerX8).HasColumnName("away_player_X8");

                entity.Property(e => e.AwayPlayerX9).HasColumnName("away_player_X9");

                entity.Property(e => e.AwayPlayerY1).HasColumnName("away_player_Y1");

                entity.Property(e => e.AwayPlayerY10).HasColumnName("away_player_Y10");

                entity.Property(e => e.AwayPlayerY11).HasColumnName("away_player_Y11");

                entity.Property(e => e.AwayPlayerY2).HasColumnName("away_player_Y2");

                entity.Property(e => e.AwayPlayerY3).HasColumnName("away_player_Y3");

                entity.Property(e => e.AwayPlayerY4).HasColumnName("away_player_Y4");

                entity.Property(e => e.AwayPlayerY5).HasColumnName("away_player_Y5");

                entity.Property(e => e.AwayPlayerY6).HasColumnName("away_player_Y6");

                entity.Property(e => e.AwayPlayerY7).HasColumnName("away_player_Y7");

                entity.Property(e => e.AwayPlayerY8).HasColumnName("away_player_Y8");

                entity.Property(e => e.AwayPlayerY9).HasColumnName("away_player_Y9");

                entity.Property(e => e.AwayTeamApiId).HasColumnName("away_team_api_id");

                entity.Property(e => e.AwayTeamGoal).HasColumnName("away_team_goal");

                entity.Property(e => e.B365a)
                    .HasColumnName("B365A")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.B365d)
                    .HasColumnName("B365D")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.B365h)
                    .HasColumnName("B365H")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Bsa)
                    .HasColumnName("BSA")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Bsd)
                    .HasColumnName("BSD")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Bsh)
                    .HasColumnName("BSH")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Bwa)
                    .HasColumnName("BWA")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Bwd)
                    .HasColumnName("BWD")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Bwh)
                    .HasColumnName("BWH")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Gba)
                    .HasColumnName("GBA")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Gbd)
                    .HasColumnName("GBD")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Gbh)
                    .HasColumnName("GBH")
                    .HasColumnType("numeric(10, 2)");


                entity.Property(e => e.HomePlayer1).HasColumnName("home_player_1");

                entity.Property(e => e.HomePlayer10).HasColumnName("home_player_10");

                entity.Property(e => e.HomePlayer11).HasColumnName("home_player_11");

                entity.Property(e => e.HomePlayer2).HasColumnName("home_player_2");

                entity.Property(e => e.HomePlayer3).HasColumnName("home_player_3");

                entity.Property(e => e.HomePlayer4).HasColumnName("home_player_4");

                entity.Property(e => e.HomePlayer5).HasColumnName("home_player_5");

                entity.Property(e => e.HomePlayer6).HasColumnName("home_player_6");

                entity.Property(e => e.HomePlayer7).HasColumnName("home_player_7");

                entity.Property(e => e.HomePlayer8).HasColumnName("home_player_8");

                entity.Property(e => e.HomePlayer9).HasColumnName("home_player_9");

                entity.Property(e => e.HomePlayerX1).HasColumnName("home_player_X1");

                entity.Property(e => e.HomePlayerX10).HasColumnName("home_player_X10");

                entity.Property(e => e.HomePlayerX11).HasColumnName("home_player_X11");

                entity.Property(e => e.HomePlayerX2).HasColumnName("home_player_X2");

                entity.Property(e => e.HomePlayerX3).HasColumnName("home_player_X3");

                entity.Property(e => e.HomePlayerX4).HasColumnName("home_player_X4");

                entity.Property(e => e.HomePlayerX5).HasColumnName("home_player_X5");

                entity.Property(e => e.HomePlayerX6).HasColumnName("home_player_X6");

                entity.Property(e => e.HomePlayerX7).HasColumnName("home_player_X7");

                entity.Property(e => e.HomePlayerX8).HasColumnName("home_player_X8");

                entity.Property(e => e.HomePlayerX9).HasColumnName("home_player_X9");

                entity.Property(e => e.HomePlayerY1).HasColumnName("home_player_Y1");

                entity.Property(e => e.HomePlayerY10).HasColumnName("home_player_Y10");

                entity.Property(e => e.HomePlayerY11).HasColumnName("home_player_Y11");

                entity.Property(e => e.HomePlayerY2).HasColumnName("home_player_Y2");

                entity.Property(e => e.HomePlayerY3).HasColumnName("home_player_Y3");

                entity.Property(e => e.HomePlayerY4).HasColumnName("home_player_Y4");

                entity.Property(e => e.HomePlayerY5).HasColumnName("home_player_Y5");

                entity.Property(e => e.HomePlayerY6).HasColumnName("home_player_Y6");

                entity.Property(e => e.HomePlayerY7).HasColumnName("home_player_Y7");

                entity.Property(e => e.HomePlayerY8).HasColumnName("home_player_Y8");

                entity.Property(e => e.HomePlayerY9).HasColumnName("home_player_Y9");

                entity.Property(e => e.HomeTeamApiId).HasColumnName("home_team_api_id");

                entity.Property(e => e.HomeTeamGoal).HasColumnName("home_team_goal");

                entity.Property(e => e.Iwa)
                    .HasColumnName("IWA")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Iwd)
                    .HasColumnName("IWD")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Iwh)
                    .HasColumnName("IWH")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Lba)
                    .HasColumnName("LBA")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Lbd)
                    .HasColumnName("LBD")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Lbh)
                    .HasColumnName("LBH")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.LeagueId).HasColumnName("league_id");

                entity.Property(e => e.MatchApiId).HasColumnName("match_api_id");

                entity.Property(e => e.Psa)
                    .HasColumnName("PSA")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Psd)
                    .HasColumnName("PSD")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Psh)
                    .HasColumnName("PSH")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Season)
                    .HasColumnName("season")
                    .HasMaxLength(25);

                entity.Property(e => e.Sja)
                    .HasColumnName("SJA")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Sjd)
                    .HasColumnName("SJD")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Sjh)
                    .HasColumnName("SJH")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Stage).HasColumnName("stage");

                entity.Property(e => e.Vca)
                    .HasColumnName("VCA")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Vcd)
                    .HasColumnName("VCD")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Vch)
                    .HasColumnName("VCH")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Wha)
                    .HasColumnName("WHA")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Whd)
                    .HasColumnName("WHD")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Whh)
                    .HasColumnName("WHH")
                    .HasColumnType("numeric(10, 2)");

                entity.HasOne(d => d.AwayPlayer1Navigation)
                    .WithMany(p => p.MatchAwayPlayer1Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.AwayPlayer1)
                    .HasConstraintName("FK__Match__away_play__7D439ABD");

                entity.HasOne(d => d.AwayPlayer10Navigation)
                    .WithMany(p => p.MatchAwayPlayer10Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.AwayPlayer10)
                    .HasConstraintName("FK__Match__away_play__04E4BC85");

                entity.HasOne(d => d.AwayPlayer11Navigation)
                    .WithMany(p => p.MatchAwayPlayer11Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.AwayPlayer11)
                    .HasConstraintName("FK__Match__away_play__00200768");

                entity.HasOne(d => d.AwayPlayer2Navigation)
                    .WithMany(p => p.MatchAwayPlayer2Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.AwayPlayer2)
                    .HasConstraintName("FK__Match__away_play__01142BA1");

                entity.HasOne(d => d.AwayPlayer3Navigation)
                    .WithMany(p => p.MatchAwayPlayer3Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.AwayPlayer3)
                    .HasConstraintName("FK__Match__away_play__03F0984C");

                entity.HasOne(d => d.AwayPlayer4Navigation)
                    .WithMany(p => p.MatchAwayPlayer4Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.AwayPlayer4)
                    .HasConstraintName("FK__Match__away_play__7E37BEF6");

                entity.HasOne(d => d.AwayPlayer5Navigation)
                    .WithMany(p => p.MatchAwayPlayer5Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.AwayPlayer5)
                    .HasConstraintName("FK__Match__away_play__7A672E12");

                entity.HasOne(d => d.AwayPlayer6Navigation)
                    .WithMany(p => p.MatchAwayPlayer6Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.AwayPlayer6)
                    .HasConstraintName("FK__Match__away_play__76969D2E");

                entity.HasOne(d => d.AwayPlayer7Navigation)
                    .WithMany(p => p.MatchAwayPlayer7Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.AwayPlayer7)
                    .HasConstraintName("FK__Match__away_play__778AC167");

                entity.HasOne(d => d.AwayPlayer8Navigation)
                    .WithMany(p => p.MatchAwayPlayer8Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.AwayPlayer8)
                    .HasConstraintName("FK__Match__away_play__7C4F7684");

                entity.HasOne(d => d.AwayPlayer9Navigation)
                    .WithMany(p => p.MatchAwayPlayer9Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.AwayPlayer9)
                    .HasConstraintName("FK__Match__away_play__787EE5A0");

                entity.HasOne(d => d.AwayTeamApi)
                    .WithMany(p => p.MatchAwayTeamApi)
                    .HasPrincipalKey(p => p.TeamApiId)
                    .HasForeignKey(d => d.AwayTeamApiId)
                    .HasConstraintName("FK__Match__away_team__02FC7413");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Match)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__Match__country_i__70DDC3D8");

                entity.HasOne(d => d.HomePlayer1Navigation)
                    .WithMany(p => p.MatchHomePlayer1Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.HomePlayer1)
                    .HasConstraintName("FK__Match__home_play__05D8E0BE");

                entity.HasOne(d => d.HomePlayer10Navigation)
                    .WithMany(p => p.MatchHomePlayer10Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.HomePlayer10)
                    .HasConstraintName("FK__Match__home_play__74AE54BC");

                entity.HasOne(d => d.HomePlayer11Navigation)
                    .WithMany(p => p.MatchHomePlayer11Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.HomePlayer11)
                    .HasConstraintName("FK__Match__home_play__06CD04F7");

                entity.HasOne(d => d.HomePlayer2Navigation)
                    .WithMany(p => p.MatchHomePlayer2Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.HomePlayer2)
                    .HasConstraintName("FK__Match__home_play__7B5B524B");

                entity.HasOne(d => d.HomePlayer3Navigation)
                    .WithMany(p => p.MatchHomePlayer3Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.HomePlayer3)
                    .HasConstraintName("FK__Match__home_play__75A278F5");

                entity.HasOne(d => d.HomePlayer4Navigation)
                    .WithMany(p => p.MatchHomePlayer4Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.HomePlayer4)
                    .HasConstraintName("FK__Match__home_play__72C60C4A");

                entity.HasOne(d => d.HomePlayer5Navigation)
                    .WithMany(p => p.MatchHomePlayer5Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.HomePlayer5)
                    .HasConstraintName("FK__Match__home_play__797309D9");

                entity.HasOne(d => d.HomePlayer6Navigation)
                    .WithMany(p => p.MatchHomePlayer6Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.HomePlayer6)
                    .HasConstraintName("FK__Match__home_play__73BA3083");

                entity.HasOne(d => d.HomePlayer7Navigation)
                    .WithMany(p => p.MatchHomePlayer7Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.HomePlayer7)
                    .HasConstraintName("FK__Match__home_play__7F2BE32F");

                entity.HasOne(d => d.HomePlayer8Navigation)
                    .WithMany(p => p.MatchHomePlayer8Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.HomePlayer8)
                    .HasConstraintName("FK__Match__home_play__02084FDA");

                entity.HasOne(d => d.HomePlayer9Navigation)
                    .WithMany(p => p.MatchHomePlayer9Navigation)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.HomePlayer9)
                    .HasConstraintName("FK__Match__home_play__07C12930");

                entity.HasOne(d => d.HomeTeamApi)
                    .WithMany(p => p.MatchHomeTeamApi)
                    .HasPrincipalKey(p => p.TeamApiId)
                    .HasForeignKey(d => d.HomeTeamApiId)
                    .HasConstraintName("FK__Match__home_team__71D1E811");

                entity.HasOne(d => d.League)
                    .WithMany(p => p.Match)
                    .HasForeignKey(d => d.LeagueId)
                    .HasConstraintName("FK__Match__league_id__6FE99F9F");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasIndex(e => e.PlayerApiId)
                    .HasName("UQ__Player__6A75280332C2A466")
                    .IsUnique();

                entity.HasIndex(e => e.PlayerFifaApiId)
                    .HasName("UQ__Player__DBDA4BBB4B262D27")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Birthday).HasColumnName("birthday");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.PlayerApiId)
                    .IsRequired()
                    .HasColumnName("player_api_id");

                entity.Property(e => e.PlayerFifaApiId)
                    .IsRequired()
                    .HasColumnName("player_fifa_api_id");

                entity.Property(e => e.PlayerName)
                    .HasColumnName("player_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Weight).HasColumnName("weight");
            });

            modelBuilder.Entity<PlayerAttributes>(entity =>
            {
                entity.ToTable("Player_Attributes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Acceleration).HasColumnName("acceleration");

                entity.Property(e => e.Aggression).HasColumnName("aggression");

                entity.Property(e => e.Agility).HasColumnName("agility");

                entity.Property(e => e.AttackingWorkRate)
                    .HasColumnName("attacking_work_rate")
                    .HasMaxLength(50);

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.BallControl).HasColumnName("ball_control");

                entity.Property(e => e.Crossing).HasColumnName("crossing");

                entity.Property(e => e.Curve).HasColumnName("curve");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.DefensiveWorkRate)
                    .HasColumnName("defensive_work_rate")
                    .HasMaxLength(50);

                entity.Property(e => e.Dribbling).HasColumnName("dribbling");

                entity.Property(e => e.Finishing).HasColumnName("finishing");

                entity.Property(e => e.FreeKickAccuracy).HasColumnName("free_kick_accuracy");

                entity.Property(e => e.GkDiving).HasColumnName("gk_diving");

                entity.Property(e => e.GkHandling).HasColumnName("gk_handling");

                entity.Property(e => e.GkKicking).HasColumnName("gk_kicking");

                entity.Property(e => e.GkPositioning).HasColumnName("gk_positioning");

                entity.Property(e => e.GkReflexes).HasColumnName("gk_reflexes");

                entity.Property(e => e.HeadingAccuracy).HasColumnName("heading_accuracy");

                entity.Property(e => e.Interceptions).HasColumnName("interceptions");

                entity.Property(e => e.Jumping).HasColumnName("jumping");

                entity.Property(e => e.LongPassing).HasColumnName("long_passing");

                entity.Property(e => e.LongShots).HasColumnName("long_shots");

                entity.Property(e => e.Marking).HasColumnName("marking");

                entity.Property(e => e.OverallRating).HasColumnName("overall_rating");

                entity.Property(e => e.Penalties).HasColumnName("penalties");

                entity.Property(e => e.PlayerApiId).HasColumnName("player_api_id");

                entity.Property(e => e.PlayerFifaApiId).HasColumnName("player_fifa_api_id");

                entity.Property(e => e.Positioning).HasColumnName("positioning");

                entity.Property(e => e.Potential).HasColumnName("potential");

                entity.Property(e => e.PreferredFoot)
                    .HasColumnName("preferred_foot")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Reactions).HasColumnName("reactions");

                entity.Property(e => e.ShortPassing).HasColumnName("short_passing");

                entity.Property(e => e.ShotPower).HasColumnName("shot_power");

                entity.Property(e => e.SlidingTackle).HasColumnName("sliding_tackle");

                entity.Property(e => e.SprintSpeed).HasColumnName("sprint_speed");

                entity.Property(e => e.Stamina).HasColumnName("stamina");

                entity.Property(e => e.StandingTackle).HasColumnName("standing_tackle");

                entity.Property(e => e.Strength).HasColumnName("strength");

                entity.Property(e => e.Vision).HasColumnName("vision");

                entity.Property(e => e.Volleys).HasColumnName("volleys");

                entity.HasOne(d => d.PlayerApi)
                    .WithMany(p => p.PlayerAttributesPlayerApi)
                    .HasPrincipalKey(p => p.PlayerApiId)
                    .HasForeignKey(d => d.PlayerApiId)
                    .HasConstraintName("FK__Player_At__playe__4316F928");

                entity.HasOne(d => d.PlayerFifaApi)
                    .WithMany(p => p.PlayerAttributesPlayerFifaApi)
                    .HasPrincipalKey(p => p.PlayerFifaApiId)
                    .HasForeignKey(d => d.PlayerFifaApiId)
                    .HasConstraintName("FK__Player_At__playe__4222D4EF");
            });

            modelBuilder.Entity<PremierLeaguePlayersAttributes>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("premier_league_players_attributes");

                entity.Property(e => e.Acceleration).HasColumnName("acceleration");

                entity.Property(e => e.Aggression).HasColumnName("aggression");

                entity.Property(e => e.Agility).HasColumnName("agility");

                entity.Property(e => e.AttackingWorkRate)
                    .HasColumnName("attacking_work_rate")
                    .HasMaxLength(50);

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.BallControl).HasColumnName("ball_control");

                entity.Property(e => e.Crossing).HasColumnName("crossing");

                entity.Property(e => e.Curve).HasColumnName("curve");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.DefensiveWorkRate)
                    .HasColumnName("defensive_work_rate")
                    .HasMaxLength(50);

                entity.Property(e => e.Dribbling).HasColumnName("dribbling");

                entity.Property(e => e.Finishing).HasColumnName("finishing");

                entity.Property(e => e.FreeKickAccuracy).HasColumnName("free_kick_accuracy");

                entity.Property(e => e.GkDiving).HasColumnName("gk_diving");

                entity.Property(e => e.GkHandling).HasColumnName("gk_handling");

                entity.Property(e => e.GkKicking).HasColumnName("gk_kicking");

                entity.Property(e => e.GkPositioning).HasColumnName("gk_positioning");

                entity.Property(e => e.GkReflexes).HasColumnName("gk_reflexes");

                entity.Property(e => e.HeadingAccuracy).HasColumnName("heading_accuracy");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Interceptions).HasColumnName("interceptions");

                entity.Property(e => e.Jumping).HasColumnName("jumping");

                entity.Property(e => e.LongPassing).HasColumnName("long_passing");

                entity.Property(e => e.LongShots).HasColumnName("long_shots");

                entity.Property(e => e.Marking).HasColumnName("marking");

                entity.Property(e => e.OverallRating).HasColumnName("overall_rating");

                entity.Property(e => e.Penalties).HasColumnName("penalties");

                entity.Property(e => e.PlayerApiId).HasColumnName("player_api_id");

                entity.Property(e => e.PlayerFifaApiId).HasColumnName("player_fifa_api_id");

                entity.Property(e => e.Positioning).HasColumnName("positioning");

                entity.Property(e => e.Potential).HasColumnName("potential");

                entity.Property(e => e.PreferredFoot)
                    .HasColumnName("preferred_foot")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Reactions).HasColumnName("reactions");

                entity.Property(e => e.ShortPassing).HasColumnName("short_passing");

                entity.Property(e => e.ShotPower).HasColumnName("shot_power");

                entity.Property(e => e.SlidingTackle).HasColumnName("sliding_tackle");

                entity.Property(e => e.SprintSpeed).HasColumnName("sprint_speed");

                entity.Property(e => e.Stamina).HasColumnName("stamina");

                entity.Property(e => e.StandingTackle).HasColumnName("standing_tackle");

                entity.Property(e => e.Strength).HasColumnName("strength");

                entity.Property(e => e.Vision).HasColumnName("vision");

                entity.Property(e => e.Volleys).HasColumnName("volleys");
            });

            modelBuilder.Entity<PremierLeaguePlayersWithAway>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("premier_league_players_with_away");

                entity.Property(e => e.Birthday).HasColumnName("birthday");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.PlayerApiId).HasColumnName("player_api_id");

                entity.Property(e => e.PlayerFifaApiId).HasColumnName("player_fifa_api_id");

                entity.Property(e => e.PlayerName)
                    .HasColumnName("player_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Weight).HasColumnName("weight");
            });

            modelBuilder.Entity<PremierLeagueTeams>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("premier_league_teams");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.TeamApiId).HasColumnName("team_api_id");

                entity.Property(e => e.TeamFifaApiId).HasColumnName("team_fifa_api_id");

                entity.Property(e => e.TeamLongName)
                    .HasColumnName("team_long_name")
                    .HasMaxLength(255);

                entity.Property(e => e.TeamShortName)
                    .HasColumnName("team_short_name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<PremierLeagueTeamsAttributes>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("premier_league_teams_attributes");

                entity.Property(e => e.BuildUpPlayDribbling).HasColumnName("buildUpPlayDribbling");

                entity.Property(e => e.BuildUpPlayDribblingClass)
                    .HasColumnName("buildUpPlayDribblingClass")
                    .HasMaxLength(50);

                entity.Property(e => e.BuildUpPlayPassing).HasColumnName("buildUpPlayPassing");

                entity.Property(e => e.BuildUpPlayPassingClass)
                    .HasColumnName("buildUpPlayPassingClass")
                    .HasMaxLength(50);

                entity.Property(e => e.BuildUpPlayPositioningClass)
                    .HasColumnName("buildUpPlayPositioningClass")
                    .HasMaxLength(50);

                entity.Property(e => e.BuildUpPlaySpeed).HasColumnName("buildUpPlaySpeed");

                entity.Property(e => e.BuildUpPlaySpeedClass)
                    .HasColumnName("buildUpPlaySpeedClass")
                    .HasMaxLength(50);

                entity.Property(e => e.ChanceCreationCrossing).HasColumnName("chanceCreationCrossing");

                entity.Property(e => e.ChanceCreationCrossingClass)
                    .HasColumnName("chanceCreationCrossingClass")
                    .HasMaxLength(50);

                entity.Property(e => e.ChanceCreationPassing).HasColumnName("chanceCreationPassing");

                entity.Property(e => e.ChanceCreationPassingClass)
                    .HasColumnName("chanceCreationPassingClass")
                    .HasMaxLength(50);

                entity.Property(e => e.ChanceCreationPositioningClass)
                    .HasColumnName("chanceCreationPositioningClass")
                    .HasMaxLength(50);

                entity.Property(e => e.ChanceCreationShooting).HasColumnName("chanceCreationShooting");

                entity.Property(e => e.ChanceCreationShootingClass)
                    .HasColumnName("chanceCreationShootingClass")
                    .HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.DefenceAggression).HasColumnName("defenceAggression");

                entity.Property(e => e.DefenceAggressionClass)
                    .HasColumnName("defenceAggressionClass")
                    .HasMaxLength(50);

                entity.Property(e => e.DefenceDefenderLineClass)
                    .HasColumnName("defenceDefenderLineClass")
                    .HasMaxLength(50);

                entity.Property(e => e.DefencePressure).HasColumnName("defencePressure");

                entity.Property(e => e.DefencePressureClass)
                    .HasColumnName("defencePressureClass")
                    .HasMaxLength(50);

                entity.Property(e => e.DefenceTeamWidth).HasColumnName("defenceTeamWidth");

                entity.Property(e => e.DefenceTeamWidthClass)
                    .HasColumnName("defenceTeamWidthClass")
                    .HasMaxLength(50);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.TeamApiId).HasColumnName("team_api_id");

                entity.Property(e => e.TeamFifaApiId).HasColumnName("team_fifa_api_id");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasIndex(e => e.TeamApiId)
                    .HasName("UQ__Team__54458F1D5BB51E7D")
                    .IsUnique();

                entity.HasIndex(e => e.TeamFifaApiId)
                    .HasName("UQ__Team__D11EF8E4292BFF2F")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TeamApiId)
                    .IsRequired()
                    .HasColumnName("team_api_id");

                entity.Property(e => e.TeamFifaApiId)
                    .IsRequired()
                    .HasColumnName("team_fifa_api_id");

                entity.Property(e => e.TeamLongName)
                    .HasColumnName("team_long_name")
                    .HasMaxLength(255);

                entity.Property(e => e.TeamShortName)
                    .HasColumnName("team_short_name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TeamAttributes>(entity =>
            {
                entity.ToTable("Team_Attributes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BuildUpPlayDribbling).HasColumnName("buildUpPlayDribbling");

                entity.Property(e => e.BuildUpPlayDribblingClass)
                    .HasColumnName("buildUpPlayDribblingClass")
                    .HasMaxLength(50);

                entity.Property(e => e.BuildUpPlayPassing).HasColumnName("buildUpPlayPassing");

                entity.Property(e => e.BuildUpPlayPassingClass)
                    .HasColumnName("buildUpPlayPassingClass")
                    .HasMaxLength(50);

                entity.Property(e => e.BuildUpPlayPositioningClass)
                    .HasColumnName("buildUpPlayPositioningClass")
                    .HasMaxLength(50);

                entity.Property(e => e.BuildUpPlaySpeed).HasColumnName("buildUpPlaySpeed");

                entity.Property(e => e.BuildUpPlaySpeedClass)
                    .HasColumnName("buildUpPlaySpeedClass")
                    .HasMaxLength(50);

                entity.Property(e => e.ChanceCreationCrossing).HasColumnName("chanceCreationCrossing");

                entity.Property(e => e.ChanceCreationCrossingClass)
                    .HasColumnName("chanceCreationCrossingClass")
                    .HasMaxLength(50);

                entity.Property(e => e.ChanceCreationPassing).HasColumnName("chanceCreationPassing");

                entity.Property(e => e.ChanceCreationPassingClass)
                    .HasColumnName("chanceCreationPassingClass")
                    .HasMaxLength(50);

                entity.Property(e => e.ChanceCreationPositioningClass)
                    .HasColumnName("chanceCreationPositioningClass")
                    .HasMaxLength(50);

                entity.Property(e => e.ChanceCreationShooting).HasColumnName("chanceCreationShooting");

                entity.Property(e => e.ChanceCreationShootingClass)
                    .HasColumnName("chanceCreationShootingClass")
                    .HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.DefenceAggression).HasColumnName("defenceAggression");

                entity.Property(e => e.DefenceAggressionClass)
                    .HasColumnName("defenceAggressionClass")
                    .HasMaxLength(50);

                entity.Property(e => e.DefenceDefenderLineClass)
                    .HasColumnName("defenceDefenderLineClass")
                    .HasMaxLength(50);

                entity.Property(e => e.DefencePressure).HasColumnName("defencePressure");

                entity.Property(e => e.DefencePressureClass)
                    .HasColumnName("defencePressureClass")
                    .HasMaxLength(50);

                entity.Property(e => e.DefenceTeamWidth).HasColumnName("defenceTeamWidth");

                entity.Property(e => e.DefenceTeamWidthClass)
                    .HasColumnName("defenceTeamWidthClass")
                    .HasMaxLength(50);

                entity.Property(e => e.TeamApiId).HasColumnName("team_api_id");

                entity.Property(e => e.TeamFifaApiId).HasColumnName("team_fifa_api_id");

                entity.HasOne(d => d.TeamApi)
                    .WithMany(p => p.TeamAttributesTeamApi)
                    .HasPrincipalKey(p => p.TeamApiId)
                    .HasForeignKey(d => d.TeamApiId)
                    .HasConstraintName("FK__Team_Attr__team___5070F446");

                entity.HasOne(d => d.TeamFifaApi)
                    .WithMany(p => p.TeamAttributesTeamFifaApi)
                    .HasPrincipalKey(p => p.TeamFifaApiId)
                    .HasForeignKey(d => d.TeamFifaApiId)
                    .HasConstraintName("FK__Team_Attr__team___4F7CD00D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
