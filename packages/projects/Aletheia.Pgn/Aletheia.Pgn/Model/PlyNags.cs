//-----------------------------------------------------------------------
// <copyright file="PlyNags.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model
{
    /// <summary>
    /// A collection of common Nags to use.
    /// </summary>
    public class PlyNags
    {
        #pragma warning disable CS1591,SA1516,SA1600

        public static PlyNag NullAnnotation => new PlyNag(0, null, "Null PlyNag.");
        public static PlyNag GoodMove => new PlyNag(1, "!", "Good Move.");
        public static PlyNag Mistake => new PlyNag(2, "?", "A mistake or Poor Move.");
        public static PlyNag BrilliantMove => new PlyNag(3, "!!", "Very good or brilliant move.");
        public static PlyNag Blunder => new PlyNag(4, "??", "Very poor move or blunder.");
        public static PlyNag InterestingMove => new PlyNag(5, "!?", "Speculative or interesting move.");
        public static PlyNag DubiousMove => new PlyNag(6, "?!", "Questionable or dubious move.");
        public static PlyNag ForcedMove => new PlyNag(7, "□", "Forced or only move.");
        public static PlyNag SingularMove => new PlyNag(8, null, "No reasonable alternatives.");
        public static PlyNag WorstMove => new PlyNag(9, null, "Worst move.");
        public static PlyNag DrawishPosition => new PlyNag(10, "=", "Drawish position or even.");
        public static PlyNag EqualChancesQuiet => new PlyNag(11, null, "Equal chances, quiet position.");
        public static PlyNag EqualChancesActive => new PlyNag(12, null, "Equal chances, active position.");
        public static PlyNag UnclearPosition => new PlyNag(13, "∞", "Unclear position");
        public static PlyNag WhiteSlightAdvantage => new PlyNag(14, "⩲", "White has a slight advantage.");
        public static PlyNag BlackSlightAdvantage => new PlyNag(15, "⩱", "Black has a slight advantage.");
        public static PlyNag WhiteModerateAdvantage => new PlyNag(16, "±", "White has a moderate advantage.");
        public static PlyNag BlackModerateAdvantage => new PlyNag(17, "∓", "Black has a moderate advantage.");
        public static PlyNag WhiteDecisiveAdvantage => new PlyNag(18, "+ −", "White has a decisive advantage.");
        public static PlyNag BlackDecisiveAdvantage => new PlyNag(19, "− +", "Black has a decisive advantage.");
        public static PlyNag WhiteCrushingAdvantage => new PlyNag(20, null, "White has a crushing advantage.");
        public static PlyNag BlackCrushingAdvantage => new PlyNag(21, null, "Black has a crushing advantage.");
        public static PlyNag WhiteZugzwang => new PlyNag(22, "⨀", "White is in zugzwang.");
        public static PlyNag BlackZugzwang => new PlyNag(23, "⨀", "Black is in zugzwang.");
        public static PlyNag WhiteSlightSpaceAdvantage => new PlyNag(24, null, "White has a slight space advantage.");
        public static PlyNag BlackSlightSpaceAdvantage => new PlyNag(25, null, "Black has a slight space advantage.");
        public static PlyNag WhiteModerateSpaceAdvantage => new PlyNag(26, null, "White has a moderate space advantage.");
        public static PlyNag BlackModerateSpaceAdvantage => new PlyNag(27, null, "Black has a moderate space advantage.");
        public static PlyNag WhiteDecisiveSpaceAdvantage => new PlyNag(28, null, "White has a decisive space advantage.");
        public static PlyNag BlackDecisiveSpaceAdvantage => new PlyNag(29, null, "Black has a decisive space advantage.");
        public static PlyNag WhiteSlightDevelopmentAdvantage => new PlyNag(30, null, "White has a slight development advantage.");
        public static PlyNag BlackSlightDevelopmentAdvantage => new PlyNag(31, null, "Black has a slight development advantage.");
        public static PlyNag WhiteModerateDevelopmentAdvantage => new PlyNag(32, "⟳", "White has a moderate development advantage.");
        public static PlyNag BlackModerateDevelopmentAdvantage => new PlyNag(33, "⟳", "Black has a moderate development advantage.");
        public static PlyNag WhiteDecisiveDevelopmentAdvantage => new PlyNag(34, null, "White has a decisive development advantage.");
        public static PlyNag BlackDecisiveDevelopmentAdvantage => new PlyNag(35, null, "Black has a decisive development advantage.");
        public static PlyNag WhiteInitiative => new PlyNag(36, "→", "White has the initiative.");
        public static PlyNag BlackInitiative => new PlyNag(37, "→", "Black has the initiative.");
        public static PlyNag WhiteLastingInitiative => new PlyNag(38, null, "White has the lasting initiative.");
        public static PlyNag BlackLastingInitiative => new PlyNag(39, null, "Black has the lasting initiative.");
        public static PlyNag WhiteAttack => new PlyNag(40, "↑", "White has the attack.");
        public static PlyNag BlackAttack => new PlyNag(41, "↑", "Black has the attack.");
        public static PlyNag WhiteInsufficientCompensation => new PlyNag(42, null, "White has insufficient compensation for the material deficit.");
        public static PlyNag BlackInsufficientCompensation => new PlyNag(43, null, "Black has insufficient compensation for the material deficit.");
        public static PlyNag WhiteSufficientCompensation => new PlyNag(44, null, "White has sufficient compensation for the material deficit.");
        public static PlyNag BlackSufficientCompensation => new PlyNag(45, null, "Black has sufficient compensation for the material deficit.");
        public static PlyNag WhiteMoreThanSufficientCompensation => new PlyNag(46, null, "White has more than sufficient compensation for the material deficit.");
        public static PlyNag BlackMoreSufficientCompensation => new PlyNag(47, null, "Black has more than sufficient compensation for the material deficit.");
        public static PlyNag WhiteSlightCenterControlAdvantage => new PlyNag(48, null, "White has a slight center control advantage.");
        public static PlyNag BlackSlightCenterControlAdvantage => new PlyNag(49, null, "Black has a slight center control advantage.");
        public static PlyNag WhiteModerateCenterControlAdvantage => new PlyNag(50, null, "White has a moderate center control advantage.");
        public static PlyNag BlackModerateCenterControlAdvantage => new PlyNag(51, null, "Black has a moderate center control advantage.");
        public static PlyNag WhiteDecisiveCenterControlAdvantage => new PlyNag(52, null, "White has a decisive center control advantage.");
        public static PlyNag BlackDecisiveCenterControlAdvantage => new PlyNag(53, null, "Black has a decisive center control advantage.");
        public static PlyNag WhiteSlightKingsideControlAdvantage => new PlyNag(54, null, "White has a slight Kingside control advantage.");
        public static PlyNag BlackSlightKingsideControlAdvantage => new PlyNag(55, null, "Black has a slight Kingside control advantage.");
        public static PlyNag WhiteModerateKingsideControlAdvantage => new PlyNag(56, null, "White has a moderate Kingside control advantage.");
        public static PlyNag BlackModerateKingsideControlAdvantage => new PlyNag(57, null, "Black has a moderate Kingside control advantage.");
        public static PlyNag WhiteDecisiveKingsideControlAdvantage => new PlyNag(58, null, "White has a decisive Kingside control advantage.");
        public static PlyNag BlackDecisiveKingsideControlAdvantage => new PlyNag(59, null, "Black has a decisive Kingside control advantage.");
        public static PlyNag WhiteSlightQueensideControlAdvantage => new PlyNag(60, null, "White has a slight Queenside control advantage.");
        public static PlyNag BlackSlightQueensideControlAdvantage => new PlyNag(61, null, "Black has a slight Queenside control advantage.");
        public static PlyNag WhiteModerateQueensideControlAdvantage => new PlyNag(62, null, "White has a moderate Queenside control advantage.");
        public static PlyNag BlackModerateQueensideControlAdvantage => new PlyNag(63, null, "Black has a moderate Queenside control advantage.");
        public static PlyNag WhiteDecisiveQueensideControlAdvantage => new PlyNag(64, null, "White has a decisive Queenside control advantage.");
        public static PlyNag BlackDecisiveQueensideControlAdvantage => new PlyNag(65, null, "Black has a decisive Queenside control advantage.");
        public static PlyNag WhiteVulnerableFirstRank => new PlyNag(66, null, "White has a vulnerable first rank.");
        public static PlyNag BlackVulnerableFirstRank => new PlyNag(67, null, "Black has a vulnerable first rank.");
        public static PlyNag WhiteWellProtectedFirstRank => new PlyNag(68, null, "White has a well protected first rank.");
        public static PlyNag BlackWellProtectedFirstRank => new PlyNag(69, null, "Black has a well protected first rank.");
        public static PlyNag WhitePoorlyProtectedKing => new PlyNag(70, null, "White has a poorly protected king.");
        public static PlyNag BlackPoorlyProtectedKing => new PlyNag(71, null, "Black has a poorly protected king.");
        public static PlyNag WhiteWellProtectedKing => new PlyNag(72, null, "White has a well protected king.");
        public static PlyNag BlackWellProtectedKing => new PlyNag(73, null, "Black has a well protected king.");
        public static PlyNag WhitePoorlyPlacedKing => new PlyNag(74, null, "White has a poorly placed king.");
        public static PlyNag BlackPoorlyPlacedKing => new PlyNag(75, null, "Black has a poorly placed king.");
        public static PlyNag WhiteWellPlacedKing => new PlyNag(76, null, "White has a well placed king.");
        public static PlyNag BlackWellPlacedKing => new PlyNag(77, null, "Black has a well placed king.");
        public static PlyNag WhiteVeryWeakPawnStructure => new PlyNag(78, null, "White has a very weak pawn structure.");
        public static PlyNag BlackVeryWeakPawnStructure => new PlyNag(79, null, "Black has a very weak pawn structure.");
        public static PlyNag WhiteModeratelyWeakPawnStructure => new PlyNag(80, null, "White has a moderately weak pawn structure.");
        public static PlyNag BlackModeratelyWeakPawnStructure => new PlyNag(81, null, "Black has a moderately weak pawn structure.");
        public static PlyNag WhiteModeratelyStrongPawnStructure => new PlyNag(82, null, "White has a moderately strong pawn structure.");
        public static PlyNag BlackModeratelyStrongPawnStructure => new PlyNag(83, null, "Black has a moderately strong pawn structure.");
        public static PlyNag WhiteVeryStrongPawnStructure => new PlyNag(84, null, "White has a very strong pawn structure.");
        public static PlyNag BlackVeryStrongPawnStructure => new PlyNag(85, null, "Black has a very strong pawn structure.");
        public static PlyNag WhitePoorKnightPlacement => new PlyNag(86, null, "White has poor knight placement.");
        public static PlyNag BlackPoorKnightPlacement => new PlyNag(87, null, "Black has poor knight placement.");
        public static PlyNag WhiteGoodKnightPlacement => new PlyNag(88, null, "White has good knight placement.");
        public static PlyNag BlackGoodKnightPlacement => new PlyNag(89, null, "Black has good knight placement.");
        public static PlyNag WhitePoorBishopPlacement => new PlyNag(90, null, "White has poor bishop placement.");
        public static PlyNag BlackPoorBishopPlacement => new PlyNag(91, null, "Black has poor bishop placement.");
        public static PlyNag WhiteGoodBishopPlacement => new PlyNag(92, null, "White has good bishop placement.");
        public static PlyNag BlackGoodBishopPlacement => new PlyNag(93, null, "Black has good bishop placement.");
        public static PlyNag WhitePoorRookPlacement => new PlyNag(94, null, "White has poor rook placement.");
        public static PlyNag BlackPoorRookPlacement => new PlyNag(95, null, "Black has poor rook placement.");
        public static PlyNag WhiteGoodRookPlacement => new PlyNag(96, null, "White has good rook placement.");
        public static PlyNag BlackGoodRookPlacement => new PlyNag(97, null, "Black has good rook placement.");
        public static PlyNag WhitePoorQueenPlacement => new PlyNag(98, null, "White has poor queen placement.");
        public static PlyNag BlackPoorQueenPlacement => new PlyNag(99, null, "Black has poor queen placement.");
        public static PlyNag WhiteGoodQueenPlacement => new PlyNag(100, null, "White has good queen placement.");
        public static PlyNag BlackGoodQueenPlacement => new PlyNag(101, null, "Black has good queen placement.");
        public static PlyNag WhitePoorPieceCoordination => new PlyNag(102, null, "White has poor piece coordination.");
        public static PlyNag BlackPoorPieceCoordination => new PlyNag(103, null, "Black has poor piece coordination.");
        public static PlyNag WhiteGoodPieceCoordination => new PlyNag(104, null, "White has good piece coordination.");
        public static PlyNag BlackGoodPieceCoordination => new PlyNag(105, null, "Black has good piece coordination.");
        public static PlyNag WhitePlayedOpeningVeryPoorly => new PlyNag(106, null, "White has played the opening very poorly.");
        public static PlyNag BlackPlayedOpeningVeryPoorly => new PlyNag(107, null, "Black has played the opening very poorly.");
        public static PlyNag WhitePlayedOpeningPoorly => new PlyNag(108, null, "White has played the opening poorly.");
        public static PlyNag BlackPlayedOpeningPoorly => new PlyNag(109, null, "Black has played the opening poorly.");
        public static PlyNag WhitePlayedOpeningWell => new PlyNag(110, null, "White has played the opening well.");
        public static PlyNag BlackPlayedOpeningWell => new PlyNag(111, null, "Black has played the opening well.");
        public static PlyNag WhitePlayedOpeningVeryWell => new PlyNag(112, null, "White has played the opening very well.");
        public static PlyNag BlackPlayedOpeningVeryWell => new PlyNag(113, null, "Black has played the opening very well.");
        public static PlyNag WhitePlayedMiddlegameVeryPoorly => new PlyNag(114, null, "White has played the middlegame very poorly.");
        public static PlyNag BlackPlayedMiddlegameVeryPoorly => new PlyNag(115, null, "Black has played the middlegame very poorly.");
        public static PlyNag WhitePlayedMiddlegamePoorly => new PlyNag(116, null, "White has played the middlegame poorly.");
        public static PlyNag BlackPlayedMiddlegamePoorly => new PlyNag(117, null, "Black has played the middlegame poorly.");
        public static PlyNag WhitePlayedMiddlegameWell => new PlyNag(118, null, "White has played the middlegame well.");
        public static PlyNag BlackPlayedMiddlegameWell => new PlyNag(119, null, "Black has played the middlegame well.");
        public static PlyNag WhitePlayedMiddlegameVeryWell => new PlyNag(120, null, "White has played the middlegame very well.");
        public static PlyNag BlackPlayedMiddlegameVeryWell => new PlyNag(121, null, "Black has played the middlegame very well.");
        public static PlyNag WhitePlayedEndgameVeryPoorly => new PlyNag(122, null, "White has played the endgame very poorly.");
        public static PlyNag BlackPlayedEndgameVeryPoorly => new PlyNag(123, null, "Black has played the endgame very poorly.");
        public static PlyNag WhitePlayedEndgamePoorly => new PlyNag(124, null, "White has played the endgame poorly.");
        public static PlyNag BlackPlayedEndgamePoorly => new PlyNag(125, null, "Black has played the endgame poorly.");
        public static PlyNag WhitePlayedEndgameWell => new PlyNag(126, null, "White has played the endgame well.");
        public static PlyNag BlackPlayedEndgameWell => new PlyNag(127, null, "Black has played the endgame well.");
        public static PlyNag WhitePlayedEndgameVeryWell => new PlyNag(128, null, "White has played the endgame very well.");
        public static PlyNag BlackPlayedEndgameVeryWell => new PlyNag(129, null, "Black has played the endgame very well.");
        public static PlyNag WhiteSlightCounterplay => new PlyNag(130, null, "White has slight counterplay.");
        public static PlyNag BlackSlightCounterplay => new PlyNag(131, null, "Black has slight counterplay.");
        public static PlyNag WhiteModerateCounterplay => new PlyNag(132, "⇆", "White has moderate counterplay.");
        public static PlyNag BlackModerateCounterplay => new PlyNag(133, "⇆", "Black has moderate counterplay.");
        public static PlyNag WhiteDecisiveCounterplay => new PlyNag(134, null, "White has decisive counterplay.");
        public static PlyNag BlackDecisiveCounterplay => new PlyNag(135, null, "Black has decisive counterplay.");
        public static PlyNag WhiteModerateTimePressure => new PlyNag(136, null, "White has moderate time control pressure.");
        public static PlyNag BlackModerateTimePressure => new PlyNag(137, null, "White has moderate time control pressure.");
        public static PlyNag WhiteSevereTimePressure => new PlyNag(138, null, "White has severe time control pressure.");
        public static PlyNag BlackSevereTimePressure => new PlyNag(139, null, "White has severe time control pressure.");

        #pragma warning restore CS1591,SA1516,SA1600
    }
}