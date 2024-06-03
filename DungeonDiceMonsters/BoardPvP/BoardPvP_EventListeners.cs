//Joel Campos
//5/31/2024
//Partial Class for BoardPvP (Event Listeners)

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    partial class BoardPvP
    {
        #region Generic Listeners
        private void OnMouseHoverSound(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Hover);
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_AppShutDownWhenClose)
            {
                Environment.Exit(Environment.ExitCode);
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            _PvPMenuRef.ReturnToPvPMenuFromGameOverBoard();
        }
        #endregion

        #region Turn Star Panel Elements
        private void btnRoll_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;
                //Send the action message to the server
                SendMessageToServer("[Roll Dice Action]|" + _CurrentGameState.ToString());

                //Perform the action
                btnRoll_Base();
            }
        }
        private void btnViewBoard_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;
                //Send the action message to the server
                SendMessageToServer("[View Board Action]|" + _CurrentGameState.ToString());

                //Perform the action
                btnViewBoard_Base();
            }
        }
        private void btnReturnToTurnMenu_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;
                //Send the action message to the server
                SendMessageToServer("[EXIT VIEW BOARD MODE]|" + _CurrentGameState.ToString());

                //Perform the action
                btnReturnToTurnMenu_Base();
            }
        }
        #endregion

        #region Board Tiles
        private void OnMouseEnterPicture_OnOverlay(object sender, EventArgs e)
        {
            //Extract the TileID from the tile in action
            PictureBox thisPicture = sender as PictureBox;
            int tileID = Convert.ToInt32(thisPicture.Tag);
            OnMouseEnterPicture(tileID);
        }
        private void OnMouseEnterPicture_OnInsidePicture(object sender, EventArgs e)
        {
            //Extract the TileID from the tile in action
            Panel thisPicture = sender as Panel;
            int tileID = Convert.ToInt32(thisPicture.Tag);
            OnMouseEnterPicture(tileID);
        }
        private void OnMouseEnterPicture(int tileID)
        {
            //Only allow this event if the user is the TURN PLAYER
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                List<GameState> StatesElegible = new List<GameState> 
                {
                    GameState.BoardViewMode,
                    GameState.MainPhaseBoard,
                    GameState.SummonCard,
                    GameState.SetCard,
                    GameState.FusionMaterialCandidateSelection,
                    GameState.FusionSummonTileSelection,
                    GameState.MovingCard,
                };

                //Send the action message to the server
                if (StatesElegible.Contains(_CurrentGameState))
                {
                    SendMessageToServer(string.Format("{0}|{1}|{2}", "[ON MOUSE ENTER TILE]", _CurrentGameState.ToString(), tileID.ToString()));

                    //Perform the action
                    OnMouseEnterPicture_Base(tileID);
                }
            }
        }
        private void OnMouseLeavePicture_OnOverlay(object sender, EventArgs e)
        {
            //Extract the TileID from the tile in action
            PictureBox thisPicture = sender as PictureBox;
            int tileID = Convert.ToInt32(thisPicture.Tag);
            OnMouseLeavePicture(tileID);
        }
        private void OnMouseLeavePicture_OnInsidePicture(object sender, EventArgs e)
        {
            //Extract the TileID from the tile in action
            Panel thisPicture = sender as Panel;
            int tileID = Convert.ToInt32(thisPicture.Tag);
            OnMouseLeavePicture(tileID);
        }
        private void OnMouseLeavePicture(int tileID)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                List<GameState> StatesElegible = new List<GameState>
                {
                    GameState.BoardViewMode,
                    GameState.MainPhaseBoard,
                    GameState.SummonCard,
                    GameState.SetCard,
                    GameState.FusionMaterialCandidateSelection,
                    GameState.FusionSummonTileSelection,
                    GameState.MovingCard,
                };

                //Send the action message to the server
                if (StatesElegible.Contains(_CurrentGameState))
                {
                    SendMessageToServer(string.Format("{0}|{1}|{2}", "[ON MOUSE LEAVE TILE]", _CurrentGameState.ToString(), tileID.ToString()));

                    //Perform the action
                    OnMouseLeavePicture_Base(tileID);
                }
            }
        }
        private void Tile_Click_OnOverlay(object sender, EventArgs e)
        {
            PictureBox thisPicture = sender as PictureBox;
            int tileID = Convert.ToInt32(thisPicture.Tag);
            Tile_Click(tileID);
        }
        private void Tile_Click_OnInsidePicture(object sender, EventArgs e)
        {
            Panel thisPicture = sender as Panel;
            int tileID = Convert.ToInt32(thisPicture.Tag);
            Tile_Click(tileID);
        }
        private void Tile_Click(int tileID)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                //Declate the Tile Object
                Tile thisTile = _Tiles[tileID];

                if (_CurrentGameState == GameState.MainPhaseBoard)
                {
                    if (thisTile.IsOccupied)
                    {
                        if (thisTile.CardInPlace.Owner == UserPlayerColor)
                        {
                            _CurrentGameState = GameState.NOINPUT;

                            //Send the action message to the server
                            SendMessageToServer(string.Format("[CLICK TILE TO ACTION]|{0}|{1}", _CurrentGameState.ToString(), tileID));

                            //Perform the action
                            TileClick_MainPhase_Base(tileID);
                        }
                        else
                        {
                            SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                        }
                    }
                }
                else if (_CurrentGameState == GameState.MovingCard)
                {
                    //check this tile is one of the candidates
                    if (_MoveCandidates.Contains(thisTile))
                    {
                        //Send the action message to the server
                        SendMessageToServer(string.Format("[CLICK TILE TO MOVE]|{0}|{1}", _CurrentGameState.ToString(), tileID));

                        //Perform the action
                        TileClick_MoveCard_Base(tileID);
                    }
                    else
                    {
                        SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                    }
                }
                else if (_CurrentGameState == GameState.SelectingAttackTarger)
                {
                    //check this tile is one of the candidates
                    bool thisIsACandidate = false;
                    for (int x = 0; x < _AttackCandidates.Count; x++)
                    {
                        if (_AttackCandidates[x].ID == tileID)
                        {
                            thisIsACandidate = true;
                            break;
                        }
                    }

                    if (thisIsACandidate)
                    {
                        _CurrentGameState = GameState.NOINPUT;

                        //Send the action message to the server
                        SendMessageToServer(string.Format("[CLICK TILE TO ATTACK]|{0}|{1}", _CurrentGameState.ToString(), tileID));

                        //Perform the action
                        TileClick_AttackTarget_Base(tileID);
                    }
                    else
                    {
                        SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                    }
                }
                else if (_CurrentGameState == GameState.SetCard)
                {
                    //check this tile is one of the candidates
                    bool thisIsACandidate = false;
                    for (int x = 0; x < _SetCandidates.Count; x++)
                    {
                        if (_SetCandidates[x].ID == tileID)
                        {
                            thisIsACandidate = true;
                            break;
                        }
                    }

                    if (thisIsACandidate)
                    {
                        _CurrentGameState = GameState.NOINPUT;

                        //Send the action message to the server
                        SendMessageToServer(string.Format("[CLICK TILE TO SET]|{0}|{1}", _CurrentGameState.ToString(), tileID));

                        //Perform the action
                        TileClick_SetCard_Base(tileID);
                    }
                    else
                    {
                        SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                    }
                }
                else if (_CurrentGameState == GameState.SummonCard)
                {
                    if (_validDimension)
                    {
                        _CurrentGameState = GameState.NOINPUT;

                        //Send the action message to the server
                        SendMessageToServer("[CLICK TILE TO SUMMON]|" + _CurrentGameState.ToString() + "|" + tileID);

                        //Perform the action
                        TileClick_SummonCard_Base(tileID);
                    }
                    else
                    {
                        SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                    }
                }
                else if (_CurrentGameState == GameState.FusionMaterialCandidateSelection)
                {
                    if (_FusionCandidateTiles.Contains(thisTile))
                    {
                        _CurrentGameState = GameState.NOINPUT;

                        //Send the action message to the server
                        SendMessageToServer(string.Format("{0}|{1}|{2}", "[CLICK TILE TO FUSION MATERIAL]", _CurrentGameState.ToString(), tileID));

                        //Perform the action
                        TileClick_FusionMaterial_Base(tileID);
                    }
                    else
                    {
                        SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                    }
                }
                else if (_CurrentGameState == GameState.FusionSummonTileSelection)
                {
                    if (_FusionSummonTiles.Contains(thisTile))
                    {
                        _CurrentGameState = GameState.NOINPUT;

                        //Send the action message to the server
                        SendMessageToServer(string.Format("{0}|{1}|{2}", "[CLICK TILE TO FUSION SUMMON]", _CurrentGameState.ToString(), tileID));

                        //Perform the action
                        TileClick_FusionSummon_Base(tileID);
                    }
                    else
                    {
                        SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                    }
                }
                else if (_CurrentGameState == GameState.EffectTargetSelection)
                {
                    if (_EffectTargetCandidates.Contains(thisTile))
                    {
                        _CurrentGameState = GameState.NOINPUT;

                        //Send the action message to the server
                        SendMessageToServer(string.Format("{0}|{1}|{2}", "[CLICK TILE TO EFFECT TARGET]", _CurrentGameState.ToString(), tileID));

                        //Perform the action
                        TileClick_EffectTarget_Base(tileID);
                    }
                    else
                    {
                        SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                    }
                }
            }
        }
        #endregion

        #region Side Bar and Dimension Menu
        private void btnEndTurn_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;

                //Send the action message to the server
                SendMessageToServer(string.Format("{0}|{1}", "[END TURN]", _CurrentGameState.ToString()));

                //Perform the action
                btnEndTurn_Base();
            }
        }
        private void btnNextForm_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            switch (_CurrentDimensionForm)
            {
                case DimensionForms.CrossBase:
                case DimensionForms.CrossRight:
                case DimensionForms.CrossLeft:
                case DimensionForms.CrossUpSideDown:
                    _CurrentDimensionForm = DimensionForms.LongBase; break;

                case DimensionForms.LongBase:
                case DimensionForms.LongRight:
                case DimensionForms.LongLeft:
                case DimensionForms.LongUpSideDown:
                case DimensionForms.LongFlippedBase:
                case DimensionForms.LongFlippedRight:
                case DimensionForms.LongFlippedLeft:
                case DimensionForms.LongFlippedUpSideDown:
                    _CurrentDimensionForm = DimensionForms.ZBase; break;

                case DimensionForms.ZBase:
                case DimensionForms.ZRight:
                case DimensionForms.ZLeft:
                case DimensionForms.ZUpSideDown:
                case DimensionForms.ZFlippedBase:
                case DimensionForms.ZFlippedRight:
                case DimensionForms.ZFlippedLeft:
                case DimensionForms.ZFlippedUpSideDown:
                    _CurrentDimensionForm = DimensionForms.TBase; break;

                case DimensionForms.TBase:
                case DimensionForms.TRight:
                case DimensionForms.TLeft:
                case DimensionForms.TUpSideDown:
                    _CurrentDimensionForm = DimensionForms.CrossBase; break;
            }

            //Send the action message to the server
            SendMessageToServer(string.Format("{0}|{1}|{2}", "[CHANGE DIMENSION SELECTION]", _CurrentGameState.ToString(), (int)_CurrentDimensionForm));

            UpdateDimensionPreview();
        }
        private void btnPreviousForm_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            switch (_CurrentDimensionForm)
            {
                case DimensionForms.CrossBase:
                case DimensionForms.CrossRight:
                case DimensionForms.CrossLeft:
                case DimensionForms.CrossUpSideDown:
                    _CurrentDimensionForm = DimensionForms.TBase; break;

                case DimensionForms.LongBase:
                case DimensionForms.LongRight:
                case DimensionForms.LongLeft:
                case DimensionForms.LongUpSideDown:
                case DimensionForms.LongFlippedBase:
                case DimensionForms.LongFlippedRight:
                case DimensionForms.LongFlippedLeft:
                case DimensionForms.LongFlippedUpSideDown:
                    _CurrentDimensionForm = DimensionForms.CrossBase; break;

                case DimensionForms.ZBase:
                case DimensionForms.ZRight:
                case DimensionForms.ZLeft:
                case DimensionForms.ZUpSideDown:
                case DimensionForms.ZFlippedBase:
                case DimensionForms.ZFlippedRight:
                case DimensionForms.ZFlippedLeft:
                case DimensionForms.ZFlippedUpSideDown:
                    _CurrentDimensionForm = DimensionForms.LongBase; break;

                case DimensionForms.TBase:
                case DimensionForms.TRight:
                case DimensionForms.TLeft:
                case DimensionForms.TUpSideDown:
                    _CurrentDimensionForm = DimensionForms.ZBase; break;
            }

            //Send the action message to the server
            SendMessageToServer(string.Format("{0}|{1}|{2}", "[CHANGE DIMENSION SELECTION]", _CurrentGameState.ToString(), (int)_CurrentDimensionForm));

            UpdateDimensionPreview();
        }
        private void btnFormFlip_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            switch (_CurrentDimensionForm)
            {
                case DimensionForms.CrossBase: break;
                case DimensionForms.CrossRight: _CurrentDimensionForm = DimensionForms.CrossLeft; break;
                case DimensionForms.CrossLeft: _CurrentDimensionForm = DimensionForms.CrossRight; break;
                case DimensionForms.CrossUpSideDown: break;

                case DimensionForms.LongBase: _CurrentDimensionForm = DimensionForms.LongFlippedBase; break;
                case DimensionForms.LongRight: _CurrentDimensionForm = DimensionForms.LongFlippedLeft; break;
                case DimensionForms.LongLeft: _CurrentDimensionForm = DimensionForms.LongFlippedRight; break;
                case DimensionForms.LongUpSideDown: _CurrentDimensionForm = DimensionForms.LongFlippedUpSideDown; break;

                case DimensionForms.LongFlippedBase: _CurrentDimensionForm = DimensionForms.LongBase; break;
                case DimensionForms.LongFlippedRight: _CurrentDimensionForm = DimensionForms.LongLeft; break;
                case DimensionForms.LongFlippedLeft: _CurrentDimensionForm = DimensionForms.LongRight; break;
                case DimensionForms.LongFlippedUpSideDown: _CurrentDimensionForm = DimensionForms.LongUpSideDown; break;

                case DimensionForms.ZBase: _CurrentDimensionForm = DimensionForms.ZFlippedBase; break;
                case DimensionForms.ZRight: _CurrentDimensionForm = DimensionForms.ZFlippedLeft; break;
                case DimensionForms.ZLeft: _CurrentDimensionForm = DimensionForms.ZFlippedRight; break;
                case DimensionForms.ZUpSideDown: _CurrentDimensionForm = DimensionForms.ZFlippedUpSideDown; break;

                case DimensionForms.ZFlippedBase: _CurrentDimensionForm = DimensionForms.ZBase; break;
                case DimensionForms.ZFlippedRight: _CurrentDimensionForm = DimensionForms.ZLeft; break;
                case DimensionForms.ZFlippedLeft: _CurrentDimensionForm = DimensionForms.ZRight; break;
                case DimensionForms.ZFlippedUpSideDown: _CurrentDimensionForm = DimensionForms.ZUpSideDown; break;

                case DimensionForms.TBase: break;
                case DimensionForms.TRight: _CurrentDimensionForm = DimensionForms.TLeft; break;
                case DimensionForms.TLeft: _CurrentDimensionForm = DimensionForms.TRight; break;
                case DimensionForms.TUpSideDown: break;
            }

            //Send the action message to the server
            SendMessageToServer(string.Format("{0}|{1}|{2}", "[CHANGE DIMENSION SELECTION]", _CurrentGameState.ToString(), (int)_CurrentDimensionForm));

            UpdateDimensionPreview();
        }
        private void BtnFormTurnRight_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            switch (_CurrentDimensionForm)
            {
                case DimensionForms.CrossBase: _CurrentDimensionForm = DimensionForms.CrossRight; break;
                case DimensionForms.CrossRight: _CurrentDimensionForm = DimensionForms.CrossUpSideDown; break;
                case DimensionForms.CrossLeft: _CurrentDimensionForm = DimensionForms.CrossBase; break;
                case DimensionForms.CrossUpSideDown: _CurrentDimensionForm = DimensionForms.CrossLeft; break;

                case DimensionForms.LongBase: _CurrentDimensionForm = DimensionForms.LongRight; break;
                case DimensionForms.LongRight: _CurrentDimensionForm = DimensionForms.LongUpSideDown; break;
                case DimensionForms.LongLeft: _CurrentDimensionForm = DimensionForms.LongBase; break;
                case DimensionForms.LongUpSideDown: _CurrentDimensionForm = DimensionForms.LongLeft; break;

                case DimensionForms.LongFlippedBase: _CurrentDimensionForm = DimensionForms.LongFlippedRight; break;
                case DimensionForms.LongFlippedRight: _CurrentDimensionForm = DimensionForms.LongFlippedUpSideDown; break;
                case DimensionForms.LongFlippedLeft: _CurrentDimensionForm = DimensionForms.LongFlippedBase; break;
                case DimensionForms.LongFlippedUpSideDown: _CurrentDimensionForm = DimensionForms.LongFlippedLeft; break;

                case DimensionForms.ZBase: _CurrentDimensionForm = DimensionForms.ZRight; break;
                case DimensionForms.ZRight: _CurrentDimensionForm = DimensionForms.ZUpSideDown; break;
                case DimensionForms.ZLeft: _CurrentDimensionForm = DimensionForms.ZBase; break;
                case DimensionForms.ZUpSideDown: _CurrentDimensionForm = DimensionForms.ZLeft; break;

                case DimensionForms.ZFlippedBase: _CurrentDimensionForm = DimensionForms.ZFlippedRight; break;
                case DimensionForms.ZFlippedRight: _CurrentDimensionForm = DimensionForms.ZFlippedUpSideDown; break;
                case DimensionForms.ZFlippedLeft: _CurrentDimensionForm = DimensionForms.ZFlippedBase; break;
                case DimensionForms.ZFlippedUpSideDown: _CurrentDimensionForm = DimensionForms.ZFlippedLeft; break;

                case DimensionForms.TBase: _CurrentDimensionForm = DimensionForms.TRight; break;
                case DimensionForms.TRight: _CurrentDimensionForm = DimensionForms.TUpSideDown; break;
                case DimensionForms.TLeft: _CurrentDimensionForm = DimensionForms.TBase; break;
                case DimensionForms.TUpSideDown: _CurrentDimensionForm = DimensionForms.TLeft; break;
            }

            //Send the action message to the server
            SendMessageToServer(string.Format("{0}|{1}|{2}", "[CHANGE DIMENSION SELECTION]", _CurrentGameState.ToString(), (int)_CurrentDimensionForm));

            UpdateDimensionPreview();
        }
        private void BtnFormTurnLeft_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            switch (_CurrentDimensionForm)
            {
                case DimensionForms.CrossBase: _CurrentDimensionForm = DimensionForms.CrossLeft; break;
                case DimensionForms.CrossRight: _CurrentDimensionForm = DimensionForms.CrossBase; break;
                case DimensionForms.CrossLeft: _CurrentDimensionForm = DimensionForms.CrossUpSideDown; break;
                case DimensionForms.CrossUpSideDown: _CurrentDimensionForm = DimensionForms.CrossRight; break;

                case DimensionForms.LongBase: _CurrentDimensionForm = DimensionForms.LongLeft; break;
                case DimensionForms.LongRight: _CurrentDimensionForm = DimensionForms.LongBase; break;
                case DimensionForms.LongLeft: _CurrentDimensionForm = DimensionForms.LongUpSideDown; break;
                case DimensionForms.LongUpSideDown: _CurrentDimensionForm = DimensionForms.LongRight; break;

                case DimensionForms.LongFlippedBase: _CurrentDimensionForm = DimensionForms.LongLeft; break;
                case DimensionForms.LongFlippedRight: _CurrentDimensionForm = DimensionForms.LongFlippedBase; break;
                case DimensionForms.LongFlippedLeft: _CurrentDimensionForm = DimensionForms.LongFlippedUpSideDown; break;
                case DimensionForms.LongFlippedUpSideDown: _CurrentDimensionForm = DimensionForms.LongFlippedRight; break;

                case DimensionForms.ZBase: _CurrentDimensionForm = DimensionForms.ZLeft; break;
                case DimensionForms.ZRight: _CurrentDimensionForm = DimensionForms.ZBase; break;
                case DimensionForms.ZLeft: _CurrentDimensionForm = DimensionForms.ZUpSideDown; break;
                case DimensionForms.ZUpSideDown: _CurrentDimensionForm = DimensionForms.ZRight; break;

                case DimensionForms.ZFlippedBase: _CurrentDimensionForm = DimensionForms.ZFlippedLeft; break;
                case DimensionForms.ZFlippedRight: _CurrentDimensionForm = DimensionForms.ZFlippedBase; break;
                case DimensionForms.ZFlippedLeft: _CurrentDimensionForm = DimensionForms.ZFlippedUpSideDown; break;
                case DimensionForms.ZFlippedUpSideDown: _CurrentDimensionForm = DimensionForms.ZFlippedRight; break;

                case DimensionForms.TBase: _CurrentDimensionForm = DimensionForms.TLeft; break;
                case DimensionForms.TRight: _CurrentDimensionForm = DimensionForms.TBase; break;
                case DimensionForms.TLeft: _CurrentDimensionForm = DimensionForms.TUpSideDown; break;
                case DimensionForms.TUpSideDown: _CurrentDimensionForm = DimensionForms.TRight; break;
            }

            //Send the action message to the server
            SendMessageToServer(string.Format("{0}|{1}|{2}", "[CHANGE DIMENSION SELECTION]", _CurrentGameState.ToString(), (int)_CurrentDimensionForm));

            UpdateDimensionPreview();
        }
        private void UpdateDimension_Base(int selectionID)
        {
            _CurrentDimensionForm = (DimensionForms)selectionID;
            UpdateDimensionPreview();
        }
        #endregion

        #region Action Menu
        private void btnActionCancel_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                _CurrentGameState = GameState.NOINPUT;

                //Send the action message to the server
                SendMessageToServer("[CLICK CANCEL ACTION MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnActionCancel_Base();
            }
        }
        private void btnActionMove_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                _CurrentGameState = GameState.NOINPUT;

                //Send the action message to the server
                SendMessageToServer("[CLICK MOVE ACTION MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnActionMove_Base();
            }
        }
        private void btnActionAttack_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                _CurrentGameState = GameState.NOINPUT;

                //Send the action message to the server
                SendMessageToServer("[CLICK ATTACK ACTION MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnActionAttack_Base();
            }
        }
        private void btnActionEffect_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                _CurrentGameState = GameState.NOINPUT;

                //Send the action message to the server
                SendMessageToServer("[CLICK EFFECT ACTION MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnActionEffect_Base();
            }
        }
        private void btnMoveMenuFinish_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                _CurrentGameState = GameState.NOINPUT;

                //Send the action message to the server
                SendMessageToServer("[CLICK FINISH MOVE MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnMoveMenuFinish_Base();
            }
        }
        private void btnMoveMenuCancel_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                _CurrentGameState = GameState.NOINPUT;

                //Send the action message to the server
                SendMessageToServer("[CLICK CANCEL MOVE MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnMoveMenuCancel_Base();
            }
        }
        private void btnAttackMenuCancel_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER)
            {
                _CurrentGameState = GameState.NOINPUT;

                //Send the action message to the server
                SendMessageToServer("[CLICK CANCEL ATTACK MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnAttackMenuCancel_Base();
            }
        }
        #endregion

        #region Battle Menu
        private void btnBattleMenuAttack_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;
                SoundServer.PlaySoundEffect(SoundEffect.Attack);

                //Send the opponent the action taken
                SendMessageToServer(string.Format("{0}|{1}|{2}", "[ATTACK!]", _CurrentGameState.ToString(), _AttackBonusCrest));

                //Hide the Attack Controls Panel
                PanelAttackControls.Visible = false;

                //Marks the ready flags
                _AttackerIsReadyToBattle = true;

                if (_AttackerIsReadyToBattle && _DefenderIsReadyToBattle)
                {
                    PerformDamageCalculation();
                }
            }
        }
        private void btnBattleMenuDefend_Click(object sender, EventArgs e)
        {
            //Hide the Defend Controls Panel immediately to prevent any input
            PanelDefendControls.Visible = false;
            SoundServer.PlaySoundEffect(SoundEffect.Attack);

            //Send the opponent the action taken
            SendMessageToServer(string.Format("{0}|{1}|{2}", "[DEFEND!]", _CurrentGameState.ToString(), _DefenseBonusCrest));

            //Marks the ready flags
            _DefenderDefended = true;
            _DefenderIsReadyToBattle = true;

            if (_AttackerIsReadyToBattle && _DefenderIsReadyToBattle)
            {
                PerformDamageCalculation();
            }
        }
        private void lblAttackerAdvMinus_Click(object sender, EventArgs e)
        {
            if (_AttackBonusCrest > 0)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                _AttackBonusCrest--;
                if (_AttackBonusCrest == 0)
                {
                    lblAttackerAdvMinus.Visible = false;
                }

                lblAttackerAdvPlus.Visible = true;
                lblAttackerBonusCrest.Text = _AttackBonusCrest.ToString();
                lblAttackerBonus.Text = string.Format("Bonus: {0}", (_AttackBonusCrest * 200));
                PlayerData AttackerData = TURNPLAYERDATA;
                Card Attacker = _AttackerTile.CardInPlace;
                lblAttackerCrestCount.Text = string.Format("[ATK] to use: {0}/{1}", (Attacker.AttackCost + _AttackBonusCrest), AttackerData.Crests_ATK);
            }
        }
        private void lblAttackerAdvPlus_Click(object sender, EventArgs e)
        {
            if (_AttackBonusCrest < 5)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                PlayerData AttackerData = TURNPLAYERDATA;
                Card Attacker = _AttackerTile.CardInPlace;

                _AttackBonusCrest++;
                if (_AttackBonusCrest == 5 || (_AttackBonusCrest + Attacker.AttackCost + 1) > AttackerData.Crests_ATK)
                {
                    lblAttackerAdvPlus.Visible = false;
                }

                lblAttackerAdvMinus.Visible = true;
                lblAttackerBonusCrest.Text = _AttackBonusCrest.ToString();
                lblAttackerBonus.Text = string.Format("Bonus: {0}", (_AttackBonusCrest * 200));
                lblAttackerCrestCount.Text = string.Format("[ATK] to use: {0}/{1}", (Attacker.AttackCost + _AttackBonusCrest), AttackerData.Crests_ATK);
            }
        }
        private void lblDefenderAdvMinus_Click(object sender, EventArgs e)
        {
            if (_DefenseBonusCrest > 0)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                _DefenseBonusCrest--;
                if (_DefenseBonusCrest == 0)
                {
                    lblDefenderAdvMinus.Visible = false;
                }

                lblDefenderAdvPlus.Visible = true;
                lblDefenderBonusCrest.Text = _DefenseBonusCrest.ToString();
                lblDefenderBonus.Text = string.Format("Bonus: {0}", (_DefenseBonusCrest * 200));
                PlayerData DefenderData = OPPONENTPLAYERDATA;
                Card Defender = _AttackTarger.CardInPlace;
                lblDefenderCrestCount.Text = string.Format("[DEF] to use: {0}/{1}", (Defender.DefenseCost + _DefenseBonusCrest), DefenderData.Crests_DEF);
            }
        }
        private void lblDefenderAdvPlus_Click(object sender, EventArgs e)
        {
            if (_DefenseBonusCrest < 5)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                PlayerData DefenderData = OPPONENTPLAYERDATA;
                Card Defender = _AttackTarger.CardInPlace;

                _DefenseBonusCrest++;
                if (_DefenseBonusCrest == 5 || (Defender.DefenseCost + _DefenseBonusCrest + 1) > DefenderData.Crests_DEF)
                {
                    lblDefenderAdvMinus.Visible = false;
                }

                lblDefenderAdvMinus.Visible = true;
                lblDefenderBonusCrest.Text = _DefenseBonusCrest.ToString();
                lblDefenderBonus.Text = string.Format("Bonus: {0}", (_DefenseBonusCrest * 200));
                lblDefenderCrestCount.Text = string.Format("[DEF] to use: {0}/{1}", (Defender.DefenseCost + _DefenseBonusCrest), DefenderData.Crests_DEF);
            }
        }
        private void btnBattleMenuDontDefend_Click(object sender, EventArgs e)
        {
            //Hide the Defend Controls Panel immediately to prevent any input
            PanelDefendControls.Visible = false;

            SoundServer.PlaySoundEffect(SoundEffect.Attack);

            //Send the opponent the action taken
            SendMessageToServer(string.Format("{0}|{1}", "[PASS!]", _CurrentGameState.ToString()));

            //Marks the ready flags
            _DefenderIsReadyToBattle = true;
            _DefenderDefended = false;

            if (_AttackerIsReadyToBattle && _DefenderIsReadyToBattle)
            {
                PerformDamageCalculation();
            }
        }
        private void btnEndBattle_Click(object sender, EventArgs e)
        {
            _CurrentGameState = GameState.NOINPUT;

            //Send the action message to the server
            SendMessageToServer("[END BATTLE]|" + _CurrentGameState.ToString());

            //Perform the action
            btnEndBattle_Base();
        }
        #endregion

        #region Effect Menu
        private void btnEffectMenuCancel_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;
                //Send the action message to the server
                SendMessageToServer("[CLICK CANCEL EFFECT MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnEffectMenuCancel_Base();
            }
        }
        private void btnActivate_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;
                //Send the action message to the server
                SendMessageToServer("[CLICK ACTIVATE EFFECT MENU]|" + _CurrentGameState.ToString());

                //Perform the action
                btnEffectMenuActivate_Base();
            }
        }
        #endregion

        #region Fusion Controls
        private void btnFusionSummon1_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;

                //Send the action message to the server
                SendMessageToServer(string.Format("[FUSION SELECTION MENU SELECT]|{0}|{1}", _CurrentGameState.ToString(), "0"));

                //Perform the action
                btnFusionSummon_Base("0");
            }
        }
        private void btnFusionSummon2_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;

                //Send the action message to the server
                SendMessageToServer(string.Format("[FUSION SELECTION MENU SELECT]|{0}|{1}", _CurrentGameState.ToString(), "1"));

                //Perform the action
                btnFusionSummon_Base("1");
            }
        }
        private void btnFusionSummon3_Click(object sender, EventArgs e)
        {
            if (UserPlayerColor == TURNPLAYER && _CurrentGameState != GameState.NOINPUT)
            {
                _CurrentGameState = GameState.NOINPUT;

                //Send the action message to the server
                SendMessageToServer(string.Format("[FUSION SELECTION MENU SELECT]|{0}|{1}", _CurrentGameState.ToString(), "2"));

                //Perform the action
                btnFusionSummon_Base("2");
            }
        }
        #endregion
    }
}
