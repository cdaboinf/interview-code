using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace interview_code.DesingClassInterviews
{
    /*
        Spot: A spot represents one block of the 8×8 grid and an optional piece.
            Piece: The basic building block of the system, every piece will be placed on a spot. 
            Piece class is an abstract class. The extended classes (Pawn, King, Queen, Rook, Knight, Bishop) 
            implements the abstracted operations.
        Board: Board is an 8×8 set of boxes containing all active chess pieces.
        Player: Player class represents one of the participants playing the game.
        Move: Represents a game move, containing the starting and ending spot. 
            The Move class will also keep track of the player who made the move.
        Game: This class controls the flow of a game. It keeps track of all the game moves, 
            which player has the current turn, and the final result of the game.
     */
    public class ChessGameDesign
    {
        
    }

    /// <summary>
    /// Spot: To represent a cell on the chess board:
    /// </summary>
    public class Spot
    {
        public Spot(int x, int y, Piece piece)
        {
            this.Piece = piece;
            this.X = x;
            this.Y = y;
        }

        public Piece Piece { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    /// <summary>
    /// Piece: An abstract class to represent common functionality of all chess pieces:
    /// </summary>
    public abstract class Piece
    {
        public Piece(bool white)
        {
            this.IsWhite = white;
        }

        public bool IsWhite { get; set; }

        public bool IsKilled { get; set; }

        public abstract bool CanMove(Board board, Spot start, Spot end);
    }

    /// <summary>
    /// Board: To represent a chess board:
    /// </summary>
    public class Board
    {
        Spot[][] boxes;

        public Board()
        {
            this.ResetBoard();
        }

        public Spot GetBoardSpot(int x, int y)
        {
            if (x < 0 || x > 7 || y < 0 || y > 7)
            {
                throw new Exception("Index out of bound");
            }

            return boxes[x][y];
        }

        public void ResetBoard()
        {
            // initialize white pieces
            boxes[0][4] = new Spot(0, 4, new Knight(true));
            //...
            // boxes[1][0] = new Spot(1, 0, new Pawn(true));
            //...

            // initialize black pieces
            boxes[7][4] = new Spot(0, 4, new Knight(false));
            //...
            // boxes[6][0] = new Spot(6, 0, new Pawn(false));
            //...

            // initialize remaining boxes without any piece
            for (int i = 2; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    boxes[i][j] = new Spot(i, j, null);
                }
            }
        }
    }

    public class Knight : Piece
    {
        public Knight(bool white) : base(white)
        {
        }

        public override bool CanMove(Board board, Spot start, Spot end)
        {
            // we can't move the piece to a Spot that 
            // has a piece of the same color
            if (end.Piece.IsWhite == this.IsWhite)
            {
                return false;
            }

            int x = Math.Abs(start.X - end.X);
            int y = Math.Abs(start.Y - end.Y);

            return x * y == 2;
        }
    }

    /// <summary>
    /// Player: An abstract class for player, it can be a human or a computer.
    /// </summary>
    public abstract class Player
    {
        public bool IsWhiteSide { get; set; }
        public bool HumanPlayer { get; set; }
    }

    public class HumanPlayer : Player
    {
        public HumanPlayer(bool isWhiteSide)
        {
            this.IsWhiteSide = isWhiteSide;
            this.HumanPlayer = true;
        }
    }

    public class ComputerPlayer : Player
    {
        public ComputerPlayer(bool isWhiteSide)
        {
            this.IsWhiteSide = isWhiteSide;
            this.HumanPlayer = false;
        }
    }

    /// <summary>
    /// Move: To represent a chess move:
    /// </summary>
    public class Move
    {
        private Player player;
        private Piece pieceMoved;
        private Piece pieceKilled;

        public Move(Player player, Spot start, Spot end)
        {
            this.player = player;
            this.Start = start;
            this.End = end;
            this.pieceMoved = start.Piece;
        }

        public Spot Start { get; }
        public Spot End { get; }

        public bool IsCastlingMove { get; set; }

        public bool CastlingMove { get; set; }

        public void SetPieceKilled(Piece piece)
        {
            piece.IsKilled = true;
        }
    }

    public enum GameStatus
    {
        ACTIVE,
        BLACK_WIN,
        WHITE_WIN,
        FORFEIT,
        STALEMATE,
        RESIGNATION
    }

    public class Game
    {
        private Player whitePlayer;
        private Player blaskPlayer;
        private Player currentPlayer;
        private Board board;

        private GameStatus Status;
        private List<Move> movesPlayed;

        public Game(Player p1, Player p2)
        {
            whitePlayer = p1.IsWhiteSide ? p1 : p2;
            blaskPlayer = whitePlayer == p1 ? p2 : p1;

            board.ResetBoard();
            currentPlayer = p1;
            movesPlayed.Clear();
        }

        public bool IsEnd()
        {
            return this.Status != GameStatus.ACTIVE;
        }

        public bool PlayerMove(Player player, int startX, int startY, int endX, int endY)
        {
            Spot startBox = board.GetBoardSpot(startX, startY);
            Spot endBox = board.GetBoardSpot(startY, endY);
            Move move = new Move(player, startBox, endBox);
            return this.MakeMove(move, player);
        }

        private bool MakeMove(Move move, Player player)
        {
            Piece sourcePiece = move.Start.Piece;
            if (sourcePiece == null)
            {
                return false;
            }

            // valid player
            if (player != currentPlayer)
            {
                return false;
            }

            if (sourcePiece.IsWhite != player.IsWhiteSide)
            {
                return false;
            }

            // valid move?
            if (!sourcePiece.CanMove(board, move.Start, move.End))
            {
                return false;
            }

            // kill?
            Piece destPiece = move.End.Piece;
            if (destPiece != null)
            {
                destPiece.IsKilled = true;
                move.SetPieceKilled(destPiece);
            }

            // store the move
            movesPlayed.Add(move);

            // move piece from the stat box to end box
            move.End.Piece = move.Start.Piece;
            move.Start.Piece = null;

            if (destPiece != null)
            {
                if (player.IsWhiteSide)
                {
                    this.Status = GameStatus.WHITE_WIN;
                }
                else
                {
                    this.Status = GameStatus.BLACK_WIN;
                }
            }

            // set the current turn to the other player
            if (this.currentPlayer == whitePlayer)
            {
                this.currentPlayer = blaskPlayer;
            }
            else
            {
                this.currentPlayer = whitePlayer;
            }

            return true;
        }
    }
}