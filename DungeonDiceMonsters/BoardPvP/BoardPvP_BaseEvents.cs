//Joel Campos
//5/31/2024
//Partial Class for BoardPvP (Event Listeners Base Methods)

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    partial class BoardPvP
    {
        #region Start Menu
        private void btnRoll_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                //Preamble set the board in the Main Phase 
                _CurrentGameState = GameState.MainPhaseBoard;
                PanelTurnStartMenu.Visible = false;

                bool IsUserTurn = (UserPlayerColor == TURNPLAYER);
                if (TURNPLAYER == PlayerColor.RED)
                {
                    _RollDiceForm = new RollDiceMenu(IsUserTurn, TURNPLAYER, RedData, this, ns);
                    Hide();
                    _RollDiceForm.Show();
                }
                else
                {
                    _RollDiceForm = new RollDiceMenu(IsUserTurn, TURNPLAYER, BlueData, this, ns);
                    Hide();
                    _RollDiceForm.Show();
                }
            }));
        }
        private void btnViewBoard_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                //Change Game State
                _CurrentGameState = GameState.BoardViewMode;
                PanelBoard.Enabled = true;
                PanelTurnStartMenu.Visible = false;
                //The "Exit Board View Menu" button will only be visible for the Turn Player
                //Also, the player on waiting will have the Action Instruction visible stating
                //the turn player is viewing the board
                if (UserPlayerColor == TURNPLAYER)
                {
                    btnReturnToTurnMenu.Visible = true;
                }
                else
                {
                    btnReturnToTurnMenu.Visible = false;
                    lblActionInstruction.Text = "Opponent is currently inspecting the board.";
                    lblActionInstruction.Visible = true;
                }
            }));
        }
        #endregion

        #region Tile Events
        private void OnMouseEnterPicture_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                Tile thisTile = _Tiles[tileId];
                if (_CurrentGameState == GameState.BoardViewMode || _CurrentGameState == GameState.MainPhaseBoard ||
                _CurrentGameState == GameState.SetCard || _CurrentGameState == GameState.FusionMaterialCandidateSelection
                || _CurrentGameState == GameState.FusionSummonTileSelection || _CurrentGameState == GameState.MovingCard 
                || _CurrentGameState == GameState.SelectingAttackTarger)
                {
                    SoundServer.PlaySoundEffect(SoundEffect.Hover);
                    thisTile.Hover();

                    UpdateDebugWindow();
                    LoadCardInfoPanel(thisTile);
                    LoadFieldTypeDisplay(thisTile, true);
                }
                else if (_CurrentGameState == GameState.SummonCard)
                {
                    //Highlight the possible dimmension
                    SoundServer.PlaySoundEffect(SoundEffect.Hover);

                    //Use the following function to get the ref to the tiles that compose the dimension
                    _dimensionTiles = thisTile.GetDimensionTiles(_CurrentDimensionForm);

                    //Check if it is valid or not (it becomes invalid if at least 1 tile is Null AND 
                    //if none of the tiles are adjecent to any other owned by the player)
                    _validDimension = Dimension.IsThisDimensionValid(_dimensionTiles, TURNPLAYER);

                    //Draw the dimension shape
                    _dimensionTiles[0].MarkDimensionSummonTile();
                    for (int x = 1; x < _dimensionTiles.Length; x++)
                    {
                        if (_dimensionTiles[x] != null) { _dimensionTiles[x].MarkDimensionTile(_validDimension); }
                    }
                }
            }));
        }
        private void OnMouseLeavePicture_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                Tile thisTile = _Tiles[tileId];
                if (_CurrentGameState == GameState.BoardViewMode || _CurrentGameState == GameState.MainPhaseBoard)
                {
                    thisTile.ReloadTileUI();
                    LoadFieldTypeDisplay(thisTile, false);
                }
                else if (_CurrentGameState == GameState.SetCard)
                {
                    thisTile.ReloadTileUI();
                    LoadFieldTypeDisplay(thisTile, false);
                    //Redraw the candudates
                    DisplaySetCandidates();
                }
                else if (_CurrentGameState == GameState.SummonCard)
                {
                    //Restore the possible dimmension tiles to their OG colors

                    //Use the following function to get the ref to the tiles that compose the dimension
                    Tile[] dimensionTiles = _Tiles[tileId].GetDimensionTiles(_CurrentDimensionForm);

                    //Reset the color of the dimensionTiles
                    for (int x = 0; x < dimensionTiles.Length; x++)
                    {
                        if (dimensionTiles[x] != null) { dimensionTiles[x].ReloadTileUI(); }
                    }
                }
                else if (_CurrentGameState == GameState.FusionMaterialCandidateSelection)
                {
                    thisTile.ReloadTileUI();
                    if (_FusionCandidateTiles.Contains(thisTile))
                    {
                        thisTile.MarkFusionMaterialTarget();
                    }
                }
                else if (_CurrentGameState == GameState.FusionSummonTileSelection)
                {
                    thisTile.ReloadTileUI();
                    if (_FusionSummonTiles.Contains(thisTile))
                    {
                        thisTile.MarkFusionSummonTile();
                    }
                }
                else if (_CurrentGameState == GameState.MovingCard)
                {
                    thisTile.ReloadTileUI();
                    if (_MoveCandidates.Contains(thisTile))
                    {
                        thisTile.MarkMoveTarget();
                    }
                }
                else if (_CurrentGameState == GameState.SelectingAttackTarger)
                {
                    thisTile.ReloadTileUI();

                    if(_AttackRangeTiles.Contains(thisTile))
                    {
                        thisTile.HighlightAttackRange();
                    }

                    if(_AttackCandidates.Contains(thisTile))
                    {
                        thisTile.MarkAttackTarget();
                    }
                }
            }));
        }
        private void TileClick_SummonCard_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                _CurrentGameState = GameState.NOINPUT;
                SoundServer.PlaySoundEffect(SoundEffect.SummonMonster);

                //Initialize the Dimension tiles again (Oppoenent's UI doesnt get them initialize them on hover)
                _dimensionTiles = _Tiles[tileId].GetDimensionTiles(_CurrentDimensionForm);

                //Dimension the tiles
                foreach (Tile tile in _dimensionTiles)
                {
                    tile.ChangeOwner(TURNPLAYER);
                }

                //Clean the UI
                lblActionInstruction.Visible = false;
                PanelDimenFormSelector.Visible = false;
                _CurrentDimensionForm = DimensionForms.CrossBase;

                //Now run the Master Summon Monster function
                CardsBeingSummoned.Clear();
                SummonMonster(_CardToBeSummon, tileId, SummonType.Normal);
            }));
        }
        private void TileClick_SetCard_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                _CurrentGameState = GameState.NOINPUT;
                SoundServer.PlaySoundEffect(SoundEffect.SetCard);

                //Set Card here
                Card thisCard = new Card(_CardsOnBoard.Count, CardDataBase.GetCardWithID(_CardToBeSet.ID), TURNPLAYER, true);
                _CardsOnBoard.Add(thisCard);
                _Tiles[tileId].SetCard(thisCard);

                //Add this action to the Player Data Bonus Records
                TURNPLAYERDATA.UpdateBonusItemRecord(BonusRecord.BonusItem.B008_ISetACard, 1);
                TURNPLAYERDATA.UpdateBonusItemRecord(BonusRecord.BonusItem.B024_MonsterPurist, 0);

                //Reset the UI of all the candidate tiles
                foreach (Tile thisTile in _SetCandidates)
                {
                    if (thisTile.ID != tileId) { thisTile.ReloadTileUI(); }
                }

                //Once this action is completed, move to the main phase
                EnterMainPhase();
            }));
        }
        private void TileClick_MainPhase_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                //Step 1: Generate the Card object on the card in action, we'll use it later.
                _CurrentTileSelected = _Tiles[tileId];
                Card thiscard = _CurrentTileSelected.CardInPlace;

                //Step 2: Open the Action menu by setting its dynamic location and making it visible
                //THIS DOESNT WORK BC IT SHUTS DOWN EVERYTHING INSIDE INCLUDING THE ACTION MENUS: PanelBoard.Enabled = false;
                PositionActionMenu();
                PanelActionMenu.Visible = true;
                btnActionCancel.Enabled = true;

                //Step 3A: Enable the Action Buttons based on the card in action
                if (UserPlayerColor == TURNPLAYER)
                {
                    //Hide the End Turn Button for the turn player 
                    btnEndTurn.Visible = false;

                    //Check if Card can move
                    if (thiscard.CanMove() && thiscard.MoveCost <= TURNPLAYERDATA.Crests_MOV)
                    {
                        btnActionMove.Enabled = true;
                        _TMPMoveCrestCount = TURNPLAYERDATA.Crests_MOV;
                        lblMoveMenuCrestCount.Text = string.Format("[MOV]x {0}", _TMPMoveCrestCount);
                    }
                    else
                    {
                        btnActionMove.Enabled = false;
                    }

                    //Check if Card can attack                   
                    if (CanCardAttack(thiscard, TURNPLAYERDATA))
                    {
                        btnActionAttack.Enabled = true;
                    }
                    else
                    {
                        btnActionAttack.Enabled = false;
                    }

                    //Check if Card has an effect to activate
                    if (DoesCardHasAnEffectToActivate(thiscard))
                    {
                        btnActionEffect.Enabled = true;
                    }
                    else
                    {
                        btnActionEffect.Enabled = false;
                    }
                }
                //Step 3B: Disable the Action Button for the non-turn player
                else
                {
                    //Show the action message to the opposite player                    
                    if (thiscard.IsFaceDown)
                    {
                        lblActionInstruction.Text = "Opponent selected a face-down card for action!";
                    }
                    else
                    {
                        lblActionInstruction.Text = string.Format("Opponent selected {0} for action!", thiscard.Name);
                    }
                    lblActionInstruction.Visible = true;
                    btnActionMove.Enabled = false;
                    btnActionAttack.Enabled = false;
                    btnActionEffect.Enabled = false;
                    btnActionMove.Enabled = false;
                    btnActionCancel.Enabled = false;
                }

                //Change the game state
                _CurrentGameState = GameState.ActionMenuDisplay;

            }));

            void PositionActionMenu()
            {
                //Set the location in relation to the Tile location and cursor location
                Point referencePoint = _CurrentTileSelected.Location;
                int X_Location = referencePoint.X;
                //IF the tile clicked is on the FAR RIGHT: Display the Action Menu on the left side of the Tile
                if (X_Location > 500)
                {
                    PanelActionMenu.Location = new Point(referencePoint.X - 83, referencePoint.Y - 25);
                }
                //OTHERWISE: Display the Action Menu on the right side of the Tile.
                else
                {
                    PanelActionMenu.Location = new Point(referencePoint.X + 48, referencePoint.Y - 25);
                }
            }
            bool CanCardAttack(Card thiscard, PlayerData TurnPlayerData)
            {
                //Determine if this card can attack if:
                // 1. It has NO CannotAttackCounters
                // 2. Has Attacks available left
                // 3. Player has enough atack crest to pay its cost
                // 4. Card is a monster

                //Disable the Attack option if: Card is not a monster OR Monster has no available attacks OR Monster's Attack Cost is higher than [ATK] available.
                if (thiscard.CannotAttackCounters > 0 || thiscard.AttacksAvaiable == 0 || thiscard.AttackCost > TurnPlayerData.Crests_ATK || thiscard.Category != Category.Monster || thiscard.IsUnderSpellbound)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            bool DoesCardHasAnEffectToActivate(Card thiscard)
            {
                if (thiscard.Category == Category.Monster && thiscard.HasIgnitionEffect)
                {
                    return true;
                }
                else if (thiscard.Category == Category.Spell && thiscard.IsFaceDown && (thiscard.HasContinuousEffect || thiscard.HasIgnitionEffect || thiscard.HasEquipEffect))
                {
                    return true;
                }
                else { return false; }
            }
        }
        private void TileClick_MoveCard_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.MoveCard);

                //Move the card to this location
                Card thiscard = _PreviousTileMove.CardInPlace;
                Tile tileToMoveInto = _Tiles[tileId];

                _PreviousTileMove.RemoveCard();
                tileToMoveInto.MoveInCard(thiscard);

                //Now clear the borders of all the candidates tiles to their og color
                for (int x = 0; x < _MoveCandidates.Count; x++)
                {
                    _MoveCandidates[x].ReloadTileUI();
                }

                //Now change the selection to this one tile
                _PreviousTileMove = tileToMoveInto;

                //Drecease the available crests to use
                _TMPMoveCrestCount -= thiscard.MoveCost;
                lblMoveMenuCrestCount.Text = string.Format("[MOV]x {0}", _TMPMoveCrestCount);

                //Enable both the Finish and Cancel buttons for the TURNPLAYER but keep them disable for the opponent
                if (UserPlayerColor == TURNPLAYER)
                {
                    btnMoveMenuFinish.Enabled = true;
                    btnMoveMenuCancel.Enabled = true;
                }
                else
                {
                    btnMoveMenuFinish.Enabled = false;
                    btnMoveMenuCancel.Enabled = false;
                }
                PlaceMoveMenu();

                //Now display the next round of move candidate if there are any MOV crest left.
                if (thiscard.MoveCost > _TMPMoveCrestCount)
                {
                    //No more available moves. do no generate more candidates
                    _MoveCandidates.Clear();
                    lblMoveMenuCrestCount.ForeColor = Color.Red;
                }
                else
                {
                    DisplayMoveCandidates();
                }
            }));
        }
        private void TileClick_AttackTarget_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                //Remove an Attack Available Counter from this card
                _AttackerTile.CardInPlace.RemoveAttackCounter();

                //Attack the card in this tile
                _AttackTarger = _Tiles[tileId];

                //Close the Attack Menu and Reset the UI of all the Attack Range Tiles
                PanelAttackMenu.Visible = false;
                foreach (Tile tile in _AttackRangeTiles)
                {
                    tile.ReloadTileUI();
                }

                //Clear the lists just in case
                _AttackRangeTiles.Clear();
                _AttackCandidates.Clear();

                //Intructions message can be hidden for both players during the battle phase
                lblActionInstruction.Visible = false;

                //if the card is facedow, flip it
                _AttackTarger.CardInPlace.FlipFaceUp();
                WaitNSeconds(1000);

                //Open the Battle Panel
                OpenBattleMenu();
                _CurrentGameState = GameState.BattlePhase;
            }));

            void OpenBattleMenu()
            {
                PanelBattleMenu.Visible = true;
                btnEndBattle.Visible = false;

                //Set the attacker's data
                Card Attacker = _AttackerTile.CardInPlace;
                ImageServer.ClearImage(PicAttacker);
                PicAttacker.BackgroundImage = ImageServer.FullCardImage(Attacker.CardID.ToString());
                lblBattleMenuATALP.Text = "LP: " + Attacker.LP;
                lblAttackerATK.Text = "ATK: " + Attacker.ATK;

                //Update the LP labels to reflect the Color owner
                if (TURNPLAYER == PlayerColor.RED)
                {
                    PanelAttackerCard.BackColor = Color.DarkRed;
                    PanelDefenderCard.BackColor = Color.MidnightBlue;
                }
                else
                {
                    PanelAttackerCard.BackColor = Color.MidnightBlue;
                    PanelDefenderCard.BackColor = Color.DarkRed;
                }

                //Set the defender's data. if the defender is a non-monster place the clear data.
                Card Defender = _AttackTarger.CardInPlace;
                if (Defender.Category == Category.Monster)
                {
                    ImageServer.ClearImage(PicDefender2);
                    PicDefender2.BackgroundImage = ImageServer.FullCardImage(Defender.CardID.ToString());
                    lblBattleMenuDEFLP.Text = "LP: " + Defender.LP;
                    lblDefenderDEF.Text = "DEF: " + Defender.DEF;
                }
                else if (Defender.Category == Category.Symbol)
                {
                    ImageServer.ClearImage(PicDefender2);
                    PicDefender2.BackgroundImage = ImageServer.FullCardSymbol(Defender.CurrentAttribute.ToString());
                    lblBattleMenuDEFLP.Text = "LP: " + Defender.LP;
                    lblDefenderDEF.Text = "DEF: 0";
                }
                else
                {
                    //At this point, if the attack target was a face down card, it was flipped face up
                    ImageServer.ClearImage(PicDefender2);
                    PicDefender2.BackgroundImage = ImageServer.FullCardImage(Defender.CardID.ToString());
                    lblBattleMenuDEFLP.Text = "LP: -";
                    lblDefenderDEF.Text = "DEF: -";
                }

                //Hide the "Destroyed" labels just in case
                PicAttackerDestroyed.Visible = false;
                PicDefenderDestroyed.Visible = false;

                //Set the initial Damage Calculation as "?"
                //Damage calculation will be done after both player set their Atk/Def
                //choices (defender has to choose if they defend if can)
                //also both player have the choice to add advantage bonuses if available
                lblBattleMenuDamage.Text = "Damage: ?";
                lblAttackerBonus.Text = "Bonus: ?";
                lblDefenderBonus.Text = "Bonus: ?";

                //Display either the ATTACK/DEFEND controls based on the user player
                if (UserPlayerColor == TURNPLAYER)
                {
                    //Display the base Attack Controls Panel
                    _AttackBonusCrest = 0;
                    lblAttackerBonus.Text = "Bonus: 0";
                    PlayerData AttackerData = TURNPLAYERDATA;
                    lblAttackerCrestCount.Text = string.Format("[ATK] to use: {0}/{1}", (Attacker.AttackCost + _AttackBonusCrest), AttackerData.Crests_ATK);
                    PanelAttackControls.Visible = true;

                    //If attacker monster has an advantage, enable the adv subpanel
                    bool AttackerHasAdvantage = HasAttributeAdvantage(Attacker, Defender);
                    if (AttackerHasAdvantage && Defender.Category == Category.Monster)
                    {
                        //Start the bonus crests at default 0
                        _AttackBonusCrest = 0;
                        lblAttackerBonusCrest.Text = "0";
                        //Show the + button at the start                    
                        lblAttackerAdvMinus.Visible = false;
                        lblAttackerAdvPlus.Visible = true;
                        PanelAttackerAdvBonus.Visible = true;
                    }
                    else
                    {
                        PanelAttackerAdvBonus.Visible = false;
                    }

                    //Show the 'waiting for opponent' label on the other ide
                    lblWaitingfordefender.Visible = true;
                    lblWaitingforattacker.Visible = false;
                    lblWaitingfordefender.Text = "Waiting for opponent...";
                    lblWaitingfordefender.ForeColor = Color.Yellow;

                    //Hide the Defend Controsl
                    PanelDefendControls.Visible = false;
                }
                else
                {
                    //Display the base Defend Controls Panel

                    if (Defender.Category == Category.Monster)
                    {
                        PanelDefendControls.Visible = true;
                        _DefenseBonusCrest = 0;
                        lblDefenderBonus.Text = "Bonus: 0";
                        PlayerData DefenderData = OPPONENTPLAYERDATA;
                        if (DefenderData.Crests_DEF == 0) { lblDefenderCrestCount.Text = "[DEF] to use: 0/0"; }
                        else { lblDefenderCrestCount.Text = string.Format("[DEF] to use: {0}/{1}", (Defender.DefenseCost + _DefenseBonusCrest), DefenderData.Crests_DEF); }
                        PanelDefendControls.Visible = true;
                        //If the defender does not have enought [DEF] to defend. Hide the "Defend" button
                        if (Defender.DefenseCost > DefenderData.Crests_DEF)
                        {
                            btnBattleMenuDefend.Visible = false;
                        }
                        else
                        {
                            btnBattleMenuDefend.Visible = true;
                        }
                        //If defender monster has an advantage, enable the adv subpanel
                        bool DefenderHasAdvantage = HasAttributeAdvantage(Defender, Attacker);
                        if (DefenderHasAdvantage && DefenderData.Crests_DEF > 0 && Defender.Category == Category.Monster)
                        {
                            _DefenseBonusCrest = 0;
                            lblDefenderBonusCrest.Text = "0";
                            //Show the + button at the start
                            lblDefenderAdvMinus.Visible = false;
                            lblDefenderAdvPlus.Visible = true;
                            PanelDefenderAdvBonus.Visible = true;
                        }
                        else
                        {
                            PanelDefenderAdvBonus.Visible = false;
                        }
                    }
                    else
                    {
                        //Enable the Defense controls but only enable the PASS option
                        //This is meant to work when defending with Symbols/Spells/Traps
                        PanelDefendControls.Visible = true;
                        _DefenseBonusCrest = 0;
                        lblDefenderBonus.Text = "Bonus: 0";
                        PlayerData DefenderData = OPPONENTPLAYERDATA;
                        lblDefenderCrestCount.Text = "[DEF] to use: 0";
                        btnBattleMenuDefend.Visible = false;
                        PanelDefenderAdvBonus.Visible = false;
                    }

                    //Show the 'waiting for opponent' label on the other ide
                    lblWaitingforattacker.Visible = true;
                    lblWaitingfordefender.Visible = false;
                    lblWaitingforattacker.Text = "Waiting for opponent...";
                    lblWaitingforattacker.ForeColor = Color.Yellow;

                    //Hide the Attack Controls
                    PanelAttackControls.Visible = false;
                }

                //Update the Phase Banner
                UpdateBanner("BattlePhase");
            }
        }
        private void TileClick_FusionMaterial_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Target);
                WaitNSeconds(1000);

                //Destroy the selected card and remove the fusion material from the _FusionMaterialsToBeUsed
                Tile thisTile = _Tiles[tileId];
                thisTile.DestroyCard();
                _FusionMaterialsToBeUsed.RemoveAt(0);

                //Save the reference to the tile of this Fusion Material so we know which tiles
                //can be selected to summon the fusion monster at
                _FusionSummonTiles.Add(thisTile);

                //reset the tiles of the summon candidates
                foreach (Tile tile in _FusionCandidateTiles)
                {
                    tile.ReloadTileUI();
                }

                //Now, if the _FusionMaterialsToBeUsed still contiains fusions materials to be selected,
                //repeat the selection process until all of them have been selected
                if (_FusionMaterialsToBeUsed.Count > 0)
                {
                    PromptPlayerToSelectFusionMaterial();
                }
                else
                {
                    //Mark the candidate tiles to summon the fusion monster at
                    DisplayFusionSummonTileCandidates();

                    //Change the game state so the turn player can select the tile to summon at
                    _CurrentGameState = GameState.FusionSummonTileSelection;
                }

            }));

            void DisplayFusionSummonTileCandidates()
            {
                foreach (Tile thisTile in _FusionSummonTiles)
                {
                    thisTile.MarkFusionSummonTile();
                }

                if (UserPlayerColor == TURNPLAYER)
                {
                    lblActionInstruction.Text = "Select Tile to Fusion Summon!";
                    lblActionInstruction.Visible = true;
                }
                else
                {
                    lblActionInstruction.Text = "Opponent is selecting the Tile to Fusion Summon.";
                    lblActionInstruction.Visible = true;
                }
            }
        }
        private void TileClick_FusionSummon_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                _CurrentGameState = GameState.NOINPUT;
                
                //Reset the tileUI of all the Tile Candidates to summon trhe card
                foreach (Tile thisTile in _FusionSummonTiles)
                {
                    thisTile.ReloadTileUI();
                }

                //Remove the fusion to be summoned from the fusion deck
                TURNPLAYERDATA.Deck.RemoveFusionAtIndex(_IndexOfFusionCardSelected);

                //Now we can discard the Polymerization card
                DestroyCard(_EffectOriginTile);

                //Now run the Master Summon Monster function
                SoundServer.PlaySoundEffect(SoundEffect.FusionSummon);
                CardsBeingSummoned.Clear();
                SummonMonster(_FusionToBeSummoned, tileId, SummonType.Fusion);
            }));
        }
        private void TileClick_EffectTarget_Base(int tileId)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Target);

                //Step 1: Reset the UI of all the target candidates
                foreach (Tile thisTile in _EffectTargetCandidates)
                {
                    thisTile.ReloadTileUI();
                }

                //Step 2: Highlight the selected target for the opponent to have clear visibility of which one was selected
                Tile TargetTile = _Tiles[tileId];
                TargetTile.HighlightTile();

                //Small delay to hear the SFX
                WaitNSeconds(1000);

                //Step 3: Go to the "Post target" method for the selected effect
                //The _CurrentPostTargetEffect will drive the direction of this method execution
                GoToPostTargetEffect(TargetTile);
            }));
        }
        #endregion

        #region Side Menu
        private void btnReturnToTurnMenu_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                //Return to the turn start menui
                _CurrentGameState = GameState.TurnStartMenu;
                btnReturnToTurnMenu.Visible = false;
                PanelTurnStartMenu.Visible = true;
                lblActionInstruction.Visible = false;
            }));
        }
        private void btnEndTurn_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                //Update the Phase Banner
                UpdateBanner("EndPhase");

                //Clean up the board
                btnEndTurn.Visible = false;
                lblActionInstruction.Visible = false;
              
                //1 turn effects are removed.
                UpdateEffectLogs("----------------------END PHAESE: Checking for 1 turn duration effects to be removed.");
                List<Effect> effectsToBeRemove = new List<Effect>();
                foreach (Effect thisEffect in _ActiveEffects)
                {
                    if (thisEffect.IsAOneTurnIgnition)
                    {
                        effectsToBeRemove.Add(thisEffect);
                    }
                }
                foreach (Effect thisEffect in effectsToBeRemove)
                {
                    UpdateEffectLogs(string.Format("Effect to be removed: [{0}] Origin Card Board ID: [{1}].", thisEffect.ID, thisEffect.OriginCard.OnBoardID));
                    RemoveEffect(thisEffect);
                }
                if (effectsToBeRemove.Count == 0) { UpdateEffectLogs("No effects to remove."); }
                UpdateEffectLogs("-----------------------------------------------------------------------------------------");

                //Check for Continuous effects that react to the end phase
                UpdateEffectLogs("----------------------END PHAESE: Checking for Continuous Effects that react to the End Phase");
                ResolveEffectsWithEndPhaseReactionTo();

                //All 1 turn data is reset for all monsters on the board
                //and All non-permanent spellbound counters are reduced.
                UpdateEffectLogs("----------------------END PHAESE: Checking for Spellbound Counters Removal");
                foreach (Card thisCard in _CardsOnBoard)
                {
                    if (!thisCard.IsDiscardted)
                    {
                        thisCard.ResetOneTurnData();
                    }
                    if (thisCard.IsUnderSpellbound && !thisCard.IsPermanentSpellbound && thisCard.Controller == TURNPLAYER)
                    {
                        thisCard.ReduceSpellboundCounter(1);
                        UpdateEffectLogs(string.Format("Spellbound counter removed for Card [{0}] OnBoardID: [{1}] Controlled by [{2}] - Spellbound Counters Left [{3}]", thisCard.Name, thisCard.OnBoardID, thisCard.Controller, thisCard.SpellboundCounter));
                        //if the card is not under a spellbound anymore, reactivate its continuous effect IF this is a FACE UP card
                        if (!thisCard.IsUnderSpellbound && !thisCard.IsFaceDown && thisCard.HasContinuousEffect)
                        {
                            UpdateEffectLogs("Card is not longer under a spellbound, reactivating its Continuous effect...");
                            ReactivateEffect(thisCard.GetContinuousEffect());
                        }
                    }
                }
                UpdateEffectLogs("-----------------------------------------------------------------------------------------");

                SoundServer.PlaySoundEffect(SoundEffect.EndTurn);
                WaitNSeconds(1000);

                //Change the TURNPLAYER
                if (TURNPLAYER == PlayerColor.RED)
                {
                    TURNPLAYER = PlayerColor.BLUE;
                    TURNPLAYERDATA = BlueData;
                    OPPONENTPLAYER = PlayerColor.RED;
                    OPPONENTPLAYERDATA = RedData;
                    //Each time the Red Player's Turn begins a new full turn starts 
                    _CurrentTurn++;
                }
                else
                {
                    TURNPLAYER = PlayerColor.RED;
                    TURNPLAYERDATA = RedData;
                    OPPONENTPLAYER = PlayerColor.BLUE;
                    OPPONENTPLAYERDATA = BlueData;
                }

                //Start the TURN at the TURN START PHASE
                LaunchTurnStartPanel();

                //Update GameState
                _CurrentGameState = GameState.TurnStartMenu;
            }));
        }
        private void btnEndBattle_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                _CurrentTileSelected.ReloadTileUI();
                PanelBattleMenu.Visible = false;
                EnterMainPhase();
            }));
        }
        private void UpdateDimension_Base(int selectionID)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                _CurrentDimensionForm = (DimensionForms)selectionID;
                UpdateDimensionPreview();
            }));
        }
        #endregion

        #region Action Menu
        private void btnActionCancel_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Cancel);
                //Close the Action menu/Card info panel and return to the MainPhase Stage 
                _CurrentTileSelected.ReloadTileUI();
                PanelActionMenu.Visible = false;

                EnterMainPhase();
            }));
        }
        private void btnActionMove_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                //Set the initial tile reference to use during the move sequence
                _InitialTileMove = _CurrentTileSelected;
                _PreviousTileMove = _InitialTileMove;

                //Hide the Action menu
                PanelActionMenu.Visible = false;

                //Generate and display the Move Tile Candidates and display the Move Menu
                DisplayMoveCandidates();
                PlaceMoveMenu();

                //Set the initial Enable status for the Move Menu Buttons
                if (UserPlayerColor == TURNPLAYER)
                {
                    //Finish button will be disable as no move have been performed yet
                    btnMoveMenuFinish.Enabled = false;
                    btnMoveMenuCancel.Enabled = true;
                }
                else
                {
                    //Both buttons will be disable for the opponent player
                    btnMoveMenuFinish.Enabled = false;
                    btnMoveMenuCancel.Enabled = false;
                    //additionally update the intruction message for the opponent
                    lblActionInstruction.Text = "Opponent is selecting a tile to move into!";
                    lblActionInstruction.Visible = true;
                }

                //The mov available crest count will show yellow for both players
                _TMPMoveCrestCount = TURNPLAYERDATA.Crests_MOV;
                lblMoveMenuCrestCount.Text = string.Format("[MOV]x {0}", _TMPMoveCrestCount);
                lblMoveMenuCrestCount.ForeColor = Color.Yellow;

                //Now display the Move Menu and update the game state
                PanelMoveMenu.Visible = true;
                _CurrentGameState = GameState.MovingCard;
            }));
        }
        private void btnActionAttack_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                //Step 1: Save the ref to the tile of the monster in action.
                _AttackerTile = _CurrentTileSelected;

                //Step 2: Generate the Tile List of all the active tile within the monster's attack range
                _AttackRangeTiles = _AttackerTile.GetAttackRangeTiles(false);
                _AttackCandidates = _AttackerTile.GetAttackRangeTiles(true);

                //Step 3: Update the UI by showing the attack range/attack candidates and the Attack Menu
                DisplayAttackCandidates(_AttackRangeTiles);
                PlaceAttackMenu();

                //Step 4: Hide the Action Menu and Update the instruction message to the opponent
                PanelActionMenu.Visible = false;
                if (UserPlayerColor != TURNPLAYER)
                {
                    lblActionInstruction.Text = "Opponent is selecting an attack target!";
                    lblActionInstruction.Visible = true;
                }

                //Finally, change the game state so the turn player is locked into selecting an attack target or cancelling the action
                _CurrentGameState = GameState.SelectingAttackTarger;
            }));

            void DisplayAttackCandidates(List<Tile> AttackRangeTiles)
            {
                //First highlight all the tiles within the attack range
                foreach (Tile thisTile in AttackRangeTiles)
                {
                    thisTile.HighlightAttackRange();    
                }
                //Then place the attack target overlay icon on the attack target candidates
                foreach (Tile tile in _AttackCandidates)
                {
                    tile.MarkAttackTarget();
                }
            }
            void PlaceAttackMenu()
            {
                Point referencePoint = _AttackerTile.Location;
                int X_Location = referencePoint.X;
                int Y_Location = referencePoint.Y;

                int newX = referencePoint.X;
                int newY = referencePoint.Y;

                //IF the tile clicked is on the FAR RIGHT: Display the Move Menu on the left side of the Tile
                if (X_Location > 500)
                {
                    newX = newX - 83;
                }
                //OTHERWISE: Display the Move Menu on the right side of the Tile.
                else
                {
                    newX = newX + 48;
                }

                //IF the tile clicked in on the TOP ROW: Display the Move Menu on the row below
                if (Y_Location < 30)
                {
                    newY = newY + 48;
                }
                //OTHERWISE: display the Move Menu on the row above
                else
                {
                    newY = newY - 27;
                }
                //Set the new location based on the mods above
                PanelAttackMenu.Location = new Point(newX, newY);
                PanelAttackMenu.Visible = true;
            }
        }
        private void btnActionEffect_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                //Step 1: Change the game state and hide the action menu
                _CurrentGameState = GameState.ActionMenuDisplay;
                PanelActionMenu.Visible = false;

                //Set the Effect Origin Tile for later use
                _EffectOriginTile = _CurrentTileSelected;

                //Step 2: Open the Effect Menu
                DisplayIgnitionEfectPanel();
            }));

            void DisplayIgnitionEfectPanel()
            {
                SoundServer.PlaySoundEffect(SoundEffect.EffectMenu);
                //Create the Effect Object,
                //This will initialize the _CardEffectToBeActivated to be use across the other methods for
                //this effect activation sequence.
                Card thisCard = _EffectOriginTile.CardInPlace;

                _CardEffectToBeActivated = null;
                if (thisCard.Category == Category.Monster && thisCard.HasIgnitionEffect)
                {
                    _CardEffectToBeActivated = thisCard.GetIgnitionEffect();
                }
                else if (thisCard.Category == Category.Spell)
                {
                    if (thisCard.HasContinuousEffect)
                    {
                        _CardEffectToBeActivated = thisCard.GetContinuousEffect();
                    }
                    else if (thisCard.HasIgnitionEffect)
                    {
                        _CardEffectToBeActivated = thisCard.GetIgnitionEffect();
                    }
                    else if (thisCard.HasEquipEffect)
                    {
                        _CardEffectToBeActivated = thisCard.GetEquipEffect();
                    }
                }

                //Now load the actual Effect Menu Panel
                if (UserPlayerColor == TURNPLAYER)
                {
                    LoadItVisible();
                }
                else
                {
                    //Load the Effect Menu with hidden info if it was face down
                    if (thisCard.IsFaceDown)
                    {
                        LoadItHidden();
                    }
                    else
                    {
                        LoadItVisible();
                    }
                }

                //Now show the effect menu itself
                btnEffectMenuCancel.Visible = true;
                PanelEffectActivationMenu.Visible = true;

                void LoadItVisible()
                {
                    //Show the full menu for the turn player
                    //Card Image
                    ImageServer.ClearImage(PicEffectMenuCardImage);
                    PicEffectMenuCardImage.Image = ImageServer.FullCardImage(thisCard.CardID.ToString());
                    //Effect Type Title
                    lblEffectMenuTittle.Text = string.Format("{0} Effect", _CardEffectToBeActivated.Type);
                    //Effect Text
                    lblEffectMenuDescriiption.Text = _CardEffectToBeActivated.EffectText;
                    //Cost
                    ImageServer.ClearImage(PicCostCrest);
                    PicCostCrest.Image = ImageServer.CrestIcon(_CardEffectToBeActivated.CrestCost.ToString());
                    lblCostAmount.Text = string.Format("x {0}", _CardEffectToBeActivated.CostAmount);
                    lblCostAmount.ForeColor = Color.White;
                    PanelCost.Visible = true;

                    //Activate button
                    if (thisCard.EffectUsedThisTurn)
                    {
                        lblActivationRequirementOutput.Text = "Effect already used this turn.";
                        lblActivationRequirementOutput.Visible = true;
                        btnActivate.Visible = false;
                        PanelCost.Visible = false;
                    }
                    else if (thisCard.IsUnderSpellbound)
                    {
                        lblActivationRequirementOutput.Text = "Spellbounded cards cannot activate their effects.";
                        lblActivationRequirementOutput.Visible = true;
                        btnActivate.Visible = false;
                        PanelCost.Visible = false;
                    }
                    else
                    {
                        //Check if the activation cost is met
                        if (IsCostMet(_CardEffectToBeActivated.CrestCost, _CardEffectToBeActivated.CostAmount))
                        {
                            //then check if the activation requirements is met
                            string ActivationRequirementStatus = GetActivationRequirementStatus(_CardEffectToBeActivated.ID);
                            if (ActivationRequirementStatus == "Requirements Met")
                            {
                                lblActivationRequirementOutput.Visible = false;
                                btnActivate.Visible = true;
                            }
                            else
                            {
                                lblActivationRequirementOutput.Text = ActivationRequirementStatus;
                                lblActivationRequirementOutput.Visible = true;
                                btnActivate.Visible = false;
                            }
                        }
                        else
                        {
                            lblActivationRequirementOutput.Text = "Cost not met.";
                            lblActivationRequirementOutput.Visible = true;
                            lblCostAmount.ForeColor = Color.Red;
                            btnActivate.Visible = false;
                        }
                    }

                    //Set the buttons ENABLE for the turn player and DISABLE for the opponent
                    if (UserPlayerColor == TURNPLAYER)
                    {
                        btnActivate.Enabled = true;
                        btnEffectMenuCancel.Enabled = true;
                    }
                    else
                    {
                        btnActivate.Enabled = false;
                        btnEffectMenuCancel.Enabled = false;
                    }
                }
                void LoadItHidden()
                {
                    //Card Image will be face down
                    ImageServer.ClearImage(PicEffectMenuCardImage);
                    PicEffectMenuCardImage.Image = ImageServer.FullCardImage("0");
                    //Effect Type Title
                    lblEffectMenuTittle.Text = "Effect";
                    //Effect Text
                    lblEffectMenuDescriiption.Text = "Hidden";
                    //Hide the cost panel regardless if the effect has a cost
                    PanelCost.Visible = false;
                    lblActivationRequirementOutput.Visible = false;
                    btnActivate.Visible = true;
                    //disable both actyion buttons to prevent the opposite player from interacting with them
                    btnActivate.Enabled = false;
                    btnEffectMenuCancel.Enabled = false;
                }
                bool IsCostMet(Crest crestCost, int amount)
                {
                    switch (crestCost)
                    {
                        case Crest.MAG: return amount <= TURNPLAYERDATA.Crests_MAG;
                        case Crest.TRAP: return amount <= TURNPLAYERDATA.Crests_TRAP;
                        case Crest.ATK: return amount <= TURNPLAYERDATA.Crests_ATK;
                        case Crest.DEF: return amount <= TURNPLAYERDATA.Crests_DEF;
                        case Crest.MOV: return amount <= TURNPLAYERDATA.Crests_MOV;
                        default: throw new Exception(string.Format("Crest undefined for Cost Met calculation. Crest: [{0}]", crestCost));
                    }
                }
                string GetActivationRequirementStatus(Effect.EffectID thisEffectID)
                {
                    switch (thisEffectID)
                    {
                        case Effect.EffectID.Polymerization_Ignition: return Polymerization_MetsRequirement();
                        case Effect.EffectID.FireKraken_Ignition: return OpponentHasAnyOneMonsterThatCanBeTarget();
                        case Effect.EffectID.ChangeOfHeart_Ignition: return OpponentHasAnyOneMonsterThatCanBeTarget();
                        case Effect.EffectID.CocoonofEvolution_Ignition: return CardHasAtLeastTurnCountersAmount(2);
                        case Effect.EffectID.CocconofUltraEvolution_Ignition: return PlayerHasOneCardNamed("Insect Queen");
                        case Effect.EffectID.BasicInsect_Ignition: return OpponentHasOneMonsterTypeThatCanBeTarget(Type.Insect);
                        case Effect.EffectID.Gokibore_Ignition: return ThereAreUnocuppiedTiles();
                        case Effect.EffectID.CockroachKnight_Ignition: return OpponentHasAnyOneMonsterThatCanBeTarget();
                        case Effect.EffectID.PinchHopper_Ingnition: return TurnPLayerHasAnyOneMonsterThatCanBeTarget();
                        case Effect.EffectID.UltimateInsectLV1_Ignition: return UltimateInsectLv1();
                        case Effect.EffectID.UltimateInsectLV3_Ignition: return UltimateInsectLv3();
                        case Effect.EffectID.UltimateInsectLV5_Ignition: return UltimateInsectLv5();
                        case Effect.EffectID.BlackPendant_Equip: return AnyOneMonsterThatCanBeTarget();
                        case Effect.EffectID.LegendarySword_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Warrior);
                        case Effect.EffectID.BeastFangs_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Beast);
                        case Effect.EffectID.VioletCrystal_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Zombie);
                        case Effect.EffectID.BookOfSecretArts_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Spellcaster);
                        case Effect.EffectID.PowerOfKaishin_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Aqua);
                        case Effect.EffectID.DarkEnergy_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Field);
                        case Effect.EffectID.LaserCannonArmon_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Insect);
                        case Effect.EffectID.VileGerms_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Plant);
                        case Effect.EffectID.SilverBowAndArrow_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Fairy);
                        case Effect.EffectID.DragonTreasure_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Dragon);
                        case Effect.EffectID.ElectroWhip_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Thunder);
                        case Effect.EffectID.MysticalMoon_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.BeastWarrior);
                        case Effect.EffectID.MachineConversionFactory_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Machine);
                        case Effect.EffectID.RaiseBodyHeat_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Dinosaur);
                        case Effect.EffectID.FollowWind_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.WingedBeast);
                        case Effect.EffectID.GridRod_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Cyberse);
                        case Effect.EffectID.PsychicSword_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Psychic);
                        case Effect.EffectID.SoulOfFire_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Pyro);
                        case Effect.EffectID.PoisonFangs_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Reptile);
                        case Effect.EffectID.EyeOfIllusion_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Illusion);
                        case Effect.EffectID.Stonehenge_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Rock);
                        case Effect.EffectID.Celestia_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Wyrm);
                        case Effect.EffectID.DeepSeaAria_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.SeaSerpent);
                        case Effect.EffectID.WhiteMirror_Equip: return AnyOneMonsterThatCanBeTargetOfType(Type.Fish);
                        case Effect.EffectID.ShinePalace_Equip: return AnyOneMonsterThatCanBeTargetOfAttribute(Attribute.LIGHT);
                        case Effect.EffectID.GustFan_Equip: return AnyOneMonsterThatCanBeTargetOfAttribute(Attribute.WIND);
                        case Effect.EffectID.Invigoration_Equip: return AnyOneMonsterThatCanBeTargetOfAttribute(Attribute.EARTH);
                        case Effect.EffectID.SwordofDarkDestruction_Equip: return AnyOneMonsterThatCanBeTargetOfAttribute(Attribute.DARK);
                        case Effect.EffectID.Salamandra_Equip: return AnyOneMonsterThatCanBeTargetOfAttribute(Attribute.FIRE);
                        case Effect.EffectID.SteelShell_Equip: return AnyOneMonsterThatCanBeTargetOfAttribute(Attribute.WATER);
                        default: return "Requirements Met";
                    }

                    string Polymerization_MetsRequirement()
                    {
                        //If the player has card in the fusion deck still               
                        if (TURNPLAYERDATA.Deck.FusionDeckSize > 0)
                        {
                            //Clear the Fusion Cards ready for fusion
                            _FusionCardsReadyForFusion[0] = false;
                            _FusionCardsReadyForFusion[1] = false;
                            _FusionCardsReadyForFusion[2] = false;
                            bool AtLeastOneFusionRequirementsMet = false;
                            //Check each card in the Fusion Deck to see if the player has the materials on the board
                            for (int i = 0; i < TURNPLAYERDATA.Deck.FusionDeckSize; i++)
                            {
                                //Create the CardInfo Object as we dont have a Card Object created yet
                                int thisFusionCardID = TURNPLAYERDATA.Deck.GetFusionCardIDAtIndex(i);
                                CardInfo thisFusionCard = CardDataBase.GetCardWithID(thisFusionCardID);

                                //Use the CardInfo to create the list of materials
                                List<string> fusionMaterials = thisFusionCard.GetFusionMaterials();

                                //Check if all the fusion materials exist on the board under the turn player's control
                                List<int> candidatesFound = new List<int>();
                                foreach (string thisMeterial in fusionMaterials)
                                {
                                    for (int x = 0; x < _CardsOnBoard.Count; x++)
                                    {
                                        Card thisCardOnTheBoard = _CardsOnBoard[x];
                                        if (thisCardOnTheBoard.Name == thisMeterial && !thisCardOnTheBoard.IsDiscardted &&
                                            thisCardOnTheBoard.Controller == TURNPLAYER && !candidatesFound.Contains(x))
                                        {
                                            candidatesFound.Add(x);
                                            break;
                                        }
                                    }
                                }

                                bool AllFusionMaterialsFound = fusionMaterials.Count == candidatesFound.Count;
                                if (AllFusionMaterialsFound)
                                {
                                    _FusionCardsReadyForFusion[i] = true;
                                    AtLeastOneFusionRequirementsMet = true;
                                }
                            }

                            if (AtLeastOneFusionRequirementsMet)
                            {
                                //Opponent will not run this requirement validation, so send the 
                                //_fusionCardsReadyForFusion candidates to the oppoenent
                                string candidate1 = _FusionCardsReadyForFusion[0].ToString();
                                string candidate2 = "False";
                                if (_FusionCardsReadyForFusion[1]) { candidate2 = _FusionCardsReadyForFusion[1].ToString(); }
                                string candidate3 = "False";
                                if (_FusionCardsReadyForFusion[2]) { candidate3 = _FusionCardsReadyForFusion[2].ToString(); }
                                SendMessageToServer(string.Format("{0}|{1}|{2}|{3}", "[READY FUSION CANDIDATES]", candidate1, candidate2, candidate3));
                                return "Requirements Met";
                            }
                            else { return "No fusion requirements met."; }
                        }
                        else { return "No cards in the Fusion Deck."; }
                    }
                    string OpponentHasAnyOneMonsterThatCanBeTarget()
                    {
                        //REQUIREMENT: Opponent must have any 1 monster on the board that can be target

                        bool monsterFound = false;
                        foreach (Card thisBoardCard in _CardsOnBoard)
                        {
                            if (!thisBoardCard.IsDiscardted && thisBoardCard.IsAMonster && thisBoardCard.Controller == OPPONENTPLAYER && thisBoardCard.CanBeTarget)
                            {
                                monsterFound = true;
                                break;
                            }
                        }

                        if (monsterFound)
                        {
                            return "Requirements Met";
                        }
                        else
                        {
                            return "No opponent monster to target.";
                        }
                    }
                    string TurnPLayerHasAnyOneMonsterThatCanBeTarget()
                    {
                        //REQUIREMENT: Turn Player must have any 1 monster on the board that can be target

                        bool monsterFound = false;
                        foreach (Card thisBoardCard in _CardsOnBoard)
                        {
                            if (!thisBoardCard.IsDiscardted && thisBoardCard.IsAMonster && thisBoardCard.Controller == TURNPLAYER && thisBoardCard.CanBeTarget)
                            {
                                monsterFound = true;
                                break;
                            }
                        }

                        if (monsterFound)
                        {
                            return "Requirements Met";
                        }
                        else
                        {
                            return "No opponent monster to target.";
                        }
                    }
                    string OpponentHasOneMonsterTypeThatCanBeTarget(Type targetType)
                    {
                        //REQUIREMENT: Opponent must have any 1 monster on the board that can be target

                        bool monsterFound = false;
                        foreach (Card thisBoardCard in _CardsOnBoard)
                        {
                            if (!thisBoardCard.IsDiscardted && thisBoardCard.Controller == OPPONENTPLAYER &&
                                thisBoardCard.CanBeTarget && thisBoardCard.Type == targetType)
                            {
                                monsterFound = true;
                                break;
                            }
                        }

                        if (monsterFound)
                        {
                            return "Requirements Met";
                        }
                        else
                        {
                            return "No opponent monster to target.";
                        }
                    }
                    string PlayerHasOneCardNamed(string name)
                    {
                        //REQUIREMENT: Owner must control an "name" card
                        bool queenFound = false;
                        foreach (Card thisBoardCard in _CardsOnBoard)
                        {
                            if (!thisBoardCard.IsDiscardted && thisBoardCard.Name == name)
                            {
                                queenFound = true;
                                break;
                            }
                        }

                        if (queenFound)
                        {
                            return "Requirements Met";
                        }
                        else
                        {
                            return string.Format("Not a \"{0}\" under your control.", name);
                        }
                    }
                    string CardHasAtLeastTurnCountersAmount(int amount)
                    {
                        if (thisCard.TurnCounters >= amount)
                        {
                            return "Requirements Met";
                        }
                        else
                        {
                            return "Not enought Turn Counters";
                        }
                    }
                    string ThereAreUnocuppiedTiles()
                    {
                        bool unoccupiedtileFound = false;
                        foreach(Tile thisTile in _Tiles)
                        {
                            if(thisTile.IsActive && !thisTile.IsOccupied)
                            {
                                unoccupiedtileFound = true;
                                break;
                            }
                        }

                        if (unoccupiedtileFound)
                        {
                            return "Requirements Met";
                        }
                        else
                        {
                            return "No available tiles.";
                        }
                    }
                    string UltimateInsectLv1()
                    {
                        if(thisCard.TurnCounters >= 1)
                        {
                            //Then check if there is no other "Ultima Insect" monster you control
                            bool anotherOneFound = false;
                            foreach(Card thisCardOnBoard in _CardsOnBoard)
                            {
                                if(!thisCardOnBoard.IsDiscardted && thisCardOnBoard != thisCard && thisCardOnBoard.Name.Contains("Ultimate Insect"))
                                {
                                    anotherOneFound = true;
                                }
                            }

                            if(anotherOneFound)
                            {
                                return "Cannot activate while there is another \"Ultimate Insect\" monster under your control.";
                            }
                            else
                            {
                                return "Requirements Met";
                            }
                        }
                        else
                        {
                            return "Not enough Turn Counters.";
                        }
                    }
                    string UltimateInsectLv3()
                    {
                        if(thisCard.WasTransformedInto)
                        {
                            if (thisCard.TurnCounters >= 2)
                            {
                                return "Requirements Met";
                            }
                            else
                            {
                                return "Not enough Turn Counters.";
                            }
                        }
                        else
                        {
                            return "Monster was not transformed into. Effect cant activate.";
                        }                        
                    }
                    string UltimateInsectLv5()
                    {
                        if (thisCard.WasTransformedInto)
                        {
                            if (thisCard.TurnCounters >= 3)
                            {
                                return "Requirements Met";
                            }
                            else
                            {
                                return "Not enough Turn Counters.";
                            }
                        }
                        else
                        {
                            return "Monster was not transformed into. Effect cant activate.";
                        }
                    }
                    string AnyOneMonsterThatCanBeTarget()
                    {
                        //REQUIREMENT: Any one monster of either player that can be target (for opponent side)

                        bool monsterFound = false;
                        foreach (Card thisBoardCard in _CardsOnBoard)
                        {
                            if (!thisBoardCard.IsDiscardted && thisBoardCard.IsAMonster)
                            {
                                if ((thisBoardCard.Controller == OPPONENTPLAYER && thisBoardCard.CanBeTarget) || thisBoardCard.Controller == TURNPLAYER)
                                {
                                    monsterFound = true;
                                    break;
                                }
                            }
                        }

                        if (monsterFound)
                        {
                            return "Requirements Met";
                        }
                        else
                        {
                            return "No valid targets.";
                        }
                    }
                    string AnyOneMonsterThatCanBeTargetOfLevelLimit(int levelLimit)
                    {
                        //REQUIREMENT: Any one monster of either player that can be target (for opponent side) with level equal or lower to the limit

                        bool monsterFound = false;
                        foreach (Card thisBoardCard in _CardsOnBoard)
                        {
                            if (!thisBoardCard.IsDiscardted && thisBoardCard.IsAMonster && thisBoardCard.Level <= levelLimit)
                            {
                                if((thisBoardCard.Controller == OPPONENTPLAYER && thisBoardCard.CanBeTarget) || thisBoardCard.Controller == TURNPLAYER)
                                {
                                    monsterFound = true;
                                    break;
                                }                                
                            }
                        }

                        if (monsterFound)
                        {
                            return "Requirements Met";
                        }
                        else
                        {
                            return "No valid targets.";
                        }
                    }                    
                    string AnyOneMonsterThatCanBeTargetOfType(Type thisType)
                    {
                        //REQUIREMENT: Any one monster of either player that can be target (for opponent side) with specific type

                        bool monsterFound = false;
                        foreach (Card thisBoardCard in _CardsOnBoard)
                        {
                            if (!thisBoardCard.IsDiscardted && thisBoardCard.IsAMonster && thisBoardCard.Type == thisType)
                            {
                                if ((thisBoardCard.Controller == OPPONENTPLAYER && thisBoardCard.CanBeTarget) || thisBoardCard.Controller == TURNPLAYER)
                                {
                                    monsterFound = true;
                                    break;
                                }
                            }
                        }

                        if (monsterFound)
                        {
                            return "Requirements Met";
                        }
                        else
                        {
                            return "No valid targets.";
                        }
                    }
                    string AnyOneMonsterThatCanBeTargetOfAttribute(Attribute thisAttribute)
                    {
                        //REQUIREMENT: Any one monster of either player that can be target (for opponent side) with the specific attribute

                        bool monsterFound = false;
                        foreach (Card thisBoardCard in _CardsOnBoard)
                        {
                            if (!thisBoardCard.IsDiscardted && thisBoardCard.IsAMonster && thisBoardCard.CurrentAttribute == thisAttribute)
                            {
                                if ((thisBoardCard.Controller == OPPONENTPLAYER && thisBoardCard.CanBeTarget) || thisBoardCard.Controller == TURNPLAYER)
                                {
                                    monsterFound = true;
                                    break;
                                }
                            }
                        }

                        if (monsterFound)
                        {
                            return "Requirements Met";
                        }
                        else
                        {
                            return "No valid targets.";
                        }
                    }
                }
            }
        }
        #endregion

        #region Move Menu
        private void btnMoveMenuCancel_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Cancel);
                //Reload the card's tile before the mouse trigers hovering another tile.
                _CurrentTileSelected.ReloadTileUI();

                //Now clear the borders of all the candidates tiles to their og color
                for (int x = 0; x < _MoveCandidates.Count; x++)
                {
                    _MoveCandidates[x].ReloadTileUI();
                }
                _MoveCandidates.Clear();

                //Move Menu can close now
                PanelMoveMenu.Visible = false;

                //Return card to the OG spot
                Card thiscard = _PreviousTileMove.CardInPlace;
                _PreviousTileMove.RemoveCard();
                _InitialTileMove.MoveInCard(thiscard);

                //Change the _current Selected card back to OG
                _CurrentTileSelected = _InitialTileMove;

                //Reload the Player info panel to reset the crest count
                LoadPlayersInfo();
                UpdateDebugWindow();

                EnterMainPhase();
            }));
        }
        private void btnMoveMenuFinish_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Confirm);
                //Now clear the borders of all the candidates tiles to their og color
                for (int x = 0; x < _MoveCandidates.Count; x++)
                {
                    _MoveCandidates[x].ReloadTileUI();
                }
                _MoveCandidates.Clear();
                PanelMoveMenu.Visible = false;

                //Flag that this card moved already this turn
                _PreviousTileMove.CardInPlace.RemoveMoveCounter();

                //Small delay to let the sound effect play
                WaitNSeconds(1000);

                //Apply the amoutn of crests used
                int amountUsed = TURNPLAYERDATA.Crests_MOV - _TMPMoveCrestCount;
                AdjustPlayerCrestCount(TURNPLAYER, Crest.MOV, -amountUsed);

                //Add the [MOV] used to the player bonus record
                TURNPLAYERDATA.UpdateBonusItemRecord(BonusRecord.BonusItem.B018_IWouldWalk, amountUsed);

                //Return to the Main Phase to end the Move Sequence
                EnterMainPhase();
            }));
        }
        #endregion

        #region Attack Menu
        private void btnAttackMenuCancel_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Cancel);

                //Reset the UI of all the Attack Range Tiles
                foreach (Tile tile in _AttackRangeTiles)
                {
                    tile.ReloadTileUI();
                }
                _AttackerTile.ReloadTileUI();

                //Clear the lists just in case
                _AttackRangeTiles.Clear();
                _AttackCandidates.Clear();

                //Close the action menu and return to the main phase
                PanelAttackMenu.Visible = false;
                EnterMainPhase();
            }));
        }
        #endregion

        #region Effect Menu
        private void btnEffectMenuActivate_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);

                //Step 1: Hide the Active/Cancel buttons to prevent any input while
                //the cost is reduce from the player's crest pool
                btnActivate.Visible = false;
                btnEffectMenuCancel.Visible = false;

                //Step 2: Flip the facedown card (if it was a facedown card)
                if (_CardEffectToBeActivated.OriginCard.IsFaceDown)
                {
                    _CardEffectToBeActivated.OriginCard.FlipFaceUp();
                    //Reveal the hidden info for the opposite player
                    if (UserPlayerColor != TURNPLAYER)
                    {
                        ImageServer.ClearImage(PicEffectMenuCardImage);
                        PicEffectMenuCardImage.Image = ImageServer.FullCardImage(_CardEffectToBeActivated.OriginCard.CardID.ToString());
                        lblEffectMenuTittle.Text = string.Format("{0} Effect", _CardEffectToBeActivated.Type);
                        lblEffectMenuDescriiption.Text = _CardEffectToBeActivated.EffectText;
                        ImageServer.ClearImage(PicCostCrest);
                        PicCostCrest.Image = ImageServer.CrestIcon(_CardEffectToBeActivated.CrestCost.ToString());
                        lblCostAmount.Text = string.Format("x {0}", _CardEffectToBeActivated.CostAmount);
                        lblCostAmount.ForeColor = Color.White;
                        PanelCost.Visible = true;
                        LoadCardInfoPanel(_CurrentTileSelected);
                    }
                }

                //Step 3: Reduce the cost from the player's crest pool
                AdjustPlayerCrestCount(TURNPLAYER, _CardEffectToBeActivated.CrestCost, -_CardEffectToBeActivated.CostAmount);

                //Step 4:Flag the Effect Activation this turn
                _CardEffectToBeActivated.OriginCard.MarkEffectUsedThisTurn();

                //Step 5: Give a small pause to allow the opposite player to see the effect revealed on their end
                WaitNSeconds(2000);

                //Step 6: Close the Effect Menu and active the effect
                PanelEffectActivationMenu.Visible = false;
                WaitNSeconds(500);
                ActivateEffect(_CardEffectToBeActivated);

                //NOTE: The ActivateEffect() will decide the direction of the next game state
                //based on the Activation method of the specific effect that will be activated.
            }));
        }
        private void btnEffectMenuCancel_Base()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SoundServer.PlaySoundEffect(SoundEffect.Cancel);

                //Close the Effect Menu and return to the main phase
                PanelEffectActivationMenu.Visible = false;
                _CurrentTileSelected.ReloadTileUI();

                //Reset UI for each player
                EnterMainPhase();
            }));
        }
        #endregion

        #region Battle Message Receivers
        private void BattleMessageReceived_Attack(int crestBonusAmount)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                _AttackBonusCrest = crestBonusAmount;
                _AttackerIsReadyToBattle = true;
                lblWaitingforattacker.ForeColor = Color.Green;
                lblWaitingforattacker.Text = "Opponent is ready!";
                if (_AttackerIsReadyToBattle && _DefenderIsReadyToBattle)
                {
                    PerformDamageCalculation();
                }
            }));
        }
        private void BattleMessageReceived_Defend(int crestBonusAmount)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                _DefenderDefended = true;
                _DefenseBonusCrest = crestBonusAmount;
                _DefenderIsReadyToBattle = true;
                lblWaitingfordefender.ForeColor = Color.Green;
                lblWaitingfordefender.Text = "Opponent is ready!";
                if (_AttackerIsReadyToBattle && _DefenderIsReadyToBattle)
                {
                    PerformDamageCalculation();
                }
            }));
        }
        private void BattleMessageReceived_Pass()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                _DefenderDefended = false;
                _DefenseBonusCrest = 0;
                _DefenderIsReadyToBattle = true;
                lblWaitingfordefender.ForeColor = Color.Green;
                lblWaitingfordefender.Text = "Opponent is ready!";
                if (_AttackerIsReadyToBattle && _DefenderIsReadyToBattle)
                {
                    PerformDamageCalculation();
                }
            }));
        }
        #endregion

        #region Fusion Selection Menu
        private void ReadyFusionCandidatesReceived(string strcandidate1, string strcandidate2, string strcandidate3)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                _FusionCardsReadyForFusion[0] = false;
                _FusionCardsReadyForFusion[1] = false;
                _FusionCardsReadyForFusion[2] = false;

                bool FusionCardSlot1Ready = Convert.ToBoolean(strcandidate1);
                bool FusionCardSlot2Ready = Convert.ToBoolean(strcandidate2);
                bool FusionCardSlot3Ready = Convert.ToBoolean(strcandidate3);

                _FusionCardsReadyForFusion[0] = FusionCardSlot1Ready;
                _FusionCardsReadyForFusion[1] = FusionCardSlot2Ready;
                _FusionCardsReadyForFusion[2] = FusionCardSlot3Ready;

            }));
        }
        private void btnFusionSummon_Base(string selectionIndex)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                //Initialize the fusion summon of card in index 0
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                _IndexOfFusionCardSelected = Convert.ToInt32(selectionIndex);
                int fusionID = TURNPLAYERDATA.Deck.GetFusionCardIDAtIndex(_IndexOfFusionCardSelected);
                _FusionToBeSummoned = CardDataBase.GetCardWithID(fusionID);
                _FusionMaterialsToBeUsed.Clear();
                _FusionSummonTiles.Clear();
                _FusionMaterialsToBeUsed = _FusionToBeSummoned.GetFusionMaterials();
                PanelFusionMonsterSelector.Visible = false;

                //Now the UI will prompt the turn player to select the first fusion material
                PromptPlayerToSelectFusionMaterial();
            }));
        }
        #endregion
    }
}