//Joel Campos
//5/31/2024
//Partial Class for BoardPvP (Effect Execution Methods)

using System;
using System.Collections.Generic;

namespace DungeonDiceMonsters
{
    partial class BoardPvP
    {
        #region Share Methods for Effects Execution
        private void ActivateEffect(Effect thisEffect)
        {
            UpdateEffectLogs(string.Format(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>Effect Activation: [{0}] - Origin Card Board ID: [{1}].Controller: [{2}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID, thisEffect.Owner));
            switch (thisEffect.ID)
            {
                case Effect.EffectID.DARKSymbol: DarkSymbol_Activation(thisEffect); break;
                case Effect.EffectID.LIGHTSymbol: LightSymbol_Activation(thisEffect); break;
                case Effect.EffectID.WATERSymbol: WaterSymbol_Activation(thisEffect); break;
                case Effect.EffectID.FIRESymbol: FireSymbol_Activation(thisEffect); break;
                case Effect.EffectID.EARTHSymbol: EarthSymbol_Activation(thisEffect); break;
                case Effect.EffectID.WINDSymbol: WindSymbol_Activation(thisEffect); break;
                case Effect.EffectID.Mountain: Mountain_Activation(thisEffect); break;
                case Effect.EffectID.Sogen: Sogen_Activation(thisEffect); break;
                case Effect.EffectID.Wasteland: Wasteland_Activation(thisEffect); break;
                case Effect.EffectID.Forest: Forest_Activation(thisEffect); break;
                case Effect.EffectID.Yami: Yami_Activation(thisEffect); break;
                case Effect.EffectID.Umi: Umi_Activation(thisEffect); break;
                case Effect.EffectID.Volcano: Volcano_Activation(thisEffect); break;
                case Effect.EffectID.Swamp: Swamp_Activation(thisEffect); break;
                case Effect.EffectID.Cyberworld: Cyberworld_Activation(thisEffect); break;
                case Effect.EffectID.Sanctuary: Sanctuary_Activation(thisEffect); break;
                case Effect.EffectID.Scrapyard: Scrapyard_Activation(thisEffect); break;
                case Effect.EffectID.MWarrior1_OnSummon: MWarrior1_OnSummonActivation(thisEffect); break;
                case Effect.EffectID.MWarrior1_Ignition: MWarrior1_IgnitionActivation(thisEffect); break;
                case Effect.EffectID.MWarrior2_OnSummon: MWarrior2_OnSummonActivation(thisEffect); break;
                case Effect.EffectID.MWarrior2_Ignition: MWarrior2_IgnitionActivation(thisEffect); break;
                case Effect.EffectID.Polymerization: Polymerization_IgnitionActivation(thisEffect); break;
                case Effect.EffectID.KarbonalaWarrior_Continuous: KarbonalaWarrior_ContinuousActivation(thisEffect); break;
                case Effect.EffectID.KarbonalaWarrior_Ignition: KarbonalaWarrior_IgnitionActivation(thisEffect); break;
                case Effect.EffectID.FireKraken: FireKraken_IgnitionActivation(thisEffect); break;
                case Effect.EffectID.HitotsumeGiant_OnSummon: HitotsumeGiant_OnSummonActivation(thisEffect); break;
                case Effect.EffectID.ThunderDragon_Continuous: ThunderDragon_Continuous(thisEffect); break;
                default: throw new Exception(string.Format("Effect ID: [{0}] does not have an Activate Effect Function"));
            }
            UpdateEffectLogs("-----------------------------------------------------------------------------------------" + Environment.NewLine);
        }
        private void RemoveEffect(Effect thisEffect)
        {
            //then remove the effect from the Board
            switch (thisEffect.ID)
            {
                case Effect.EffectID.ThunderDragon_Continuous: ThunderDragon_RemoveEffect(thisEffect); break;
                case Effect.EffectID.FireKraken: FireKraken_RemoveEffect(thisEffect); break;
                default: throw new Exception(string.Format("This effect id: [{0}] does not have a Remove Effect method assigned", thisEffect.ID));
            }
            //Remove the effect from the Active effect list
            _ActiveEffects.Remove(thisEffect);
        }
        private void DisplayOnSummonEffectPanel(Effect thisEffect)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectMenu);
            //Load the menu with the On Summon effect
            PicEffectMenuCardImage.Image = ImageServer.FullCardImage(thisEffect.OriginCard.CardID.ToString());
            lblEffectMenuTittle.Text = "Summon Effect";
            lblEffectMenuDescriiption.Text = thisEffect.OriginCard.OnSummonEffectText;
            //Hide the elements not needed.
            lblActivationRequirementOutput.Text = "";
            PanelCost.Visible = false;
            btnActivate.Visible = false;
            btnEffectMenuCancel.Visible = false;
            //Wait 2 sec for players to reach the effect
            PanelEffectActivationMenu.Visible = true;
            WaitNSeconds(2000);
        }
        private void DisplayOnSummonContinuousEffectPanel(Effect thisEffect)
        {
            PicEffectMenuCardImage.Image = ImageServer.FullCardImage(thisEffect.OriginCard.CardID.ToString());
            lblEffectMenuTittle.Text = "Continuous Effect";
            lblEffectMenuDescriiption.Text = thisEffect.OriginCard.ContinuousEffectText;
            //Hide the elements not needed.
            lblActivationRequirementOutput.Text = "";
            PanelCost.Visible = false;
            btnActivate.Visible = false;
            btnEffectMenuCancel.Visible = false;
            //Wait 2 sec for players to reach the effect
            PanelEffectActivationMenu.Visible = true;
            WaitNSeconds(2000);
        }
        private void HideEffectMenuPanel()
        {
            //Now you can close the On Summon Panel
            PanelEffectActivationMenu.Visible = false;
        }
        private void InitializeEffectTargetSelection()
        {
            //Step 0: _EffectTargetCandidates list has already been initialize by the effect activation method

            //Step 1: Highlight and place the "target" overlay icon in the tiles of the target candidates
            foreach (Tile thisCandidate in _EffectTargetCandidates)
            {
                thisCandidate.MarkEffectTarget();
            }

            //Step 2: Update the Instruction Message for both players
            if (UserPlayerColor == TURNPLAYER)
            {
                lblActionInstruction.Text = "Select a target!";
            }
            else
            {
                lblActionInstruction.Text = "Opponent is selecting a target!";
            }
            lblActionInstruction.Visible = true;

            //Step 3: Change the game state so the player can make its action
            _CurrentGameState = GameState.EffectTargetSelection;
        }
        private void GoToPostTargetEffect(Tile TargetTile)
        {
            UpdateEffectLogs(string.Format("Target Tile selected: [{0}] | Target Card: [{1}] with On Board ID: [{2}].Controller: [{3}]", TargetTile.ID.ToString(), TargetTile.CardInPlace.Name, TargetTile.CardInPlace.OnBoardID, TargetTile.CardInPlace.Controller));

            switch (_CurrentPostTargetState)
            {
                case PostTargetState.FireKrakenEffect: FireKraken_PostTargetEffect(TargetTile); break;
            }
        }
        private void ResolveEffectsWithAttributeChangeReactionTo(Card targetCard, Effect modifierEffect)
        {
            UpdateEffectLogs(string.Format(">>>>>>>>>>>>>>>>>>>>>>>Card [{0}] with On Board Id [{1}] Attribute was changed. Checking for active effects that react to it.", targetCard.Name, targetCard.OnBoardID));

            //NOTE REGARDING: "modifierEffect" argument
            //Some effect reactions are going to need to know which effect was the one that created the Attribut change on the monster
            //in order to validate if they react to it or not.
            //For example: Fire Kraken will change the attribute of a monster but that same action will trigger a reaction check on attribute change
            //which Fire Kraken WILL do. We do not want Fire Kraken's effect to react to its own effect lol so I need to know which is the effect that
            //cause the modification, and if that effect is the same as the one being validating for reaction, it should NOT REACT.

            //Attribute changes can make active effects to be override and thus removed from the active effects list
            //Use the following list to flag those effects and after all the effects in the active effect lists have been check for reaction,
            //remove them.
            //WE CANNOT REMOVE EFFECTS FROM THE _ActiveEffect LIST WHILE THE FOREACH LOOP IS ACTIVE
            _EffectsToBeRemovedByAttributeChangeReaction.Clear();

            foreach (Effect thisActiveEffect in _ActiveEffects)
            {
                if (thisActiveEffect.ReactsToAttributeChange)
                {
                    UpdateEffectLogs(string.Format("Reaction Check for Effect: [{0}] Origin Card Board ID: [{1}]", thisActiveEffect.ID, thisActiveEffect.OriginCard.OnBoardID));
                    switch (thisActiveEffect.ID)
                    {
                        case Effect.EffectID.DARKSymbol: DarkSymbol_ReactTo_AttributeChange(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.LIGHTSymbol: LightSymbol_ReactTo_AttributeChange(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.WATERSymbol: WaterSymbol_ReactTo_AttributeChange(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.FIRESymbol: FireSymbol_ReactTo_AttributeChange(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.EARTHSymbol: EarthSymbol_ReactTo_AttributeChange(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.WINDSymbol: WindSymbol_ReactTo_AttributeChange(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.FireKraken: FireKraken_ReactTo_AttributeChange(thisActiveEffect, targetCard, modifierEffect); break;
                        default: throw new Exception(string.Format("Effect ID: [{0}] does not have an [ReactTo_AttributeChange] Function", thisActiveEffect.ID));
                    }
                }
            }

            //Now safely remove any effects that need to be remove
            foreach (Effect thisEffectToRemove in _EffectsToBeRemovedByAttributeChangeReaction)
            {
                _ActiveEffects.Remove(thisEffectToRemove);
                UpdateEffectLogs(string.Format("The Effect [{0}] by card on board: [{1}] was remove from the active effect list.", thisEffectToRemove.ID, thisEffectToRemove.OriginCard.OnBoardID));
            }

            UpdateEffectLogs("-----------------------------------------------------------------------------------------" + Environment.NewLine);
        }
        private void PromptPlayerToSelectFusionMaterial()
        {
            //Prompts the player to select the fusion material on index 0 
            //in the _FusionMaterialsToBeUsed list

            //Step 1: Set the card name of the Fusion Material that will be selected
            string FusionMaterial = _FusionMaterialsToBeUsed[0];

            //Step 2: Display on the UI the Fusion Material candidates
            DisplayFusionMaterialCandidates();
            if (UserPlayerColor == TURNPLAYER)
            {
                lblActionInstruction.Text = string.Format("Select a [{0}] as fusion material!", FusionMaterial);
                lblActionInstruction.Visible = true;
            }
            else
            {
                lblActionInstruction.Text = "Opponent is selecting a fusion material!";
                lblActionInstruction.Visible = true;
            }

            //Step 3: Change the game state so the turn player can select the tile of the candidate
            PanelBoard.Enabled = true;
            _CurrentGameState = GameState.FusionMaterialCandidateSelection;

            void DisplayFusionMaterialCandidates()
            {
                //Just in case, reset the tile UI of the previous list
                if (_FusionCandidateTiles.Count > 0)
                {
                    foreach (Tile thisTile in _FusionCandidateTiles)
                    {
                        thisTile.ReloadTileUI();
                    }
                }


                //Generate the Tile list candidates for this Fusion Materials
                _FusionCandidateTiles.Clear();
                foreach (Tile thisTile in _Tiles)
                {
                    if (thisTile.IsOccupied)
                    {
                        if (thisTile.CardInPlace.Name == FusionMaterial && thisTile.CardInPlace.Controller == TURNPLAYER)
                        {
                            _FusionCandidateTiles.Add(thisTile);
                        }
                    }
                }

                //Now mark the Candidates
                foreach (Tile thisTile in _FusionCandidateTiles)
                {
                    thisTile.MarkFusionMaterialTarget();
                }
            }
        }
        #endregion

        #region "Dark Symbol"
        private void DarkSymbol_Activation(Effect thisEffect)
        {
            //EFFECT DESCRIPTION
            //Increase the ATK of all your DARK monsters on the board by 200.
            //During activation find all existing targets and give them the ATK increase.
            //This effect will reach to all summons: if the summon is the.Controller's monster and it a DARK Attribute: affect it.                       

            //Step 1: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterSummon = true;
            thisEffect.ReactsToAttributeChange = true;
            thisEffect.ReactsToMonsterControlChange = true;

            bool effectAppliedToAtLeastOneCard = false;
            //Step 2: Resolve the effect initial activation
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (thisCard.Controller == thisEffect.Owner)
                {
                    if (!thisCard.IsASymbol && thisCard.CurrentAttribute == Attribute.DARK)
                    {
                        effectAppliedToAtLeastOneCard = true;
                        thisCard.AdjustAttackBonus(200);
                        thisEffect.AddAffectedByCard(thisCard);
                        UpdateEffectLogs(string.Format("Effect Applied to: [{0}] On Board ID: [{1}] Owned by [{2}] - Card is DARK Attribute and Owned by the effect.Controller. Increase its ATK by 200.", thisCard.Name, thisCard.OnBoardID, thisCard.Controller));
                    }
                }
            }

            if (!effectAppliedToAtLeastOneCard) { UpdateEffectLogs("No Cards were affected by it."); }

            //Step 3: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);
        }
        private void DarkSymbol_ReactTo_NewMonsterUnderYourControl(Effect thisEffect, Card targetCard)
        {
            //If the monster summon is from the same.Controller AND...
            if (targetCard.Controller == thisEffect.Owner)
            {
                //is DARK Attribute...
                if (targetCard.CurrentAttribute == Attribute.DARK)
                {
                    //Give a 200 Attack Boost
                    targetCard.AdjustAttackBonus(200);
                    thisEffect.AddAffectedByCard(targetCard);
                    UpdateEffectLogs(string.Format("Effect Reacted: Summoned Card is DARK Attribute and Owned by the effect.Controller. Increase its ATK by 200."));
                }
            }
        }
        private void DarkSymbol_ReactTo_AttributeChange(Effect thisEffect, Card targetCard)
        {
            //If the target card that changed its attributed was the Card affected by this Dark Symbol's effect
            //then check if this Card needs a stat boost modification

            //if the target card WAS already in the Dark Symbol's effect's affected by list and the target card is NOT longer DARK,
            //then reduce the attack boost and remove the target card from the affected by list
            if (thisEffect.AffectedByList.Contains(targetCard) && targetCard.CurrentAttribute != Attribute.DARK)
            {
                targetCard.AdjustAttackBonus(-200);
                thisEffect.RemoveAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is not longer a DARK attribute monster. The effects of Dark Symbol do not apply to it anymore.");
            }
            //if the target card was NOT in the Dark Symbol's effect's affected by list and the target card is NOW DARK and both have the same.Controller
            //then increase the attack boost and add the target card to the affected by list
            else if (!thisEffect.AffectedByList.Contains(targetCard) && targetCard.CurrentAttribute == Attribute.DARK && thisEffect.Owner == targetCard.Controller)
            {
                targetCard.AdjustAttackBonus(200);
                thisEffect.AddAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is now a DARK attribute monster. Dark Symbol's effect will now apply to this card.");
            }
            else
            {
                UpdateEffectLogs("Effect did not react.");
            }
        }
        #endregion

        #region "LIGHT Symbol"
        private void LightSymbol_Activation(Effect thisEffect)
        {
            //EFFECT DESCRIPTION
            //Increase the ATK of all your LIGHT monsters on the board by 200.
            //During activation find all existing targets and give them the ATK increase.
            //This effect will reach to all summons: if the summon is the.Controller's monster and it a LIGHT Attribute: affect it.            

            //Step 1: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterSummon = true;
            thisEffect.ReactsToAttributeChange = true;
            thisEffect.ReactsToMonsterControlChange = true;

            bool effectAppliedToAtLeastOneCard = false;
            //Step 2: Resolve the effect initial activation
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (thisCard.Controller == thisEffect.Owner)
                {
                    if (!thisCard.IsASymbol && thisCard.CurrentAttribute == Attribute.LIGHT)
                    {
                        effectAppliedToAtLeastOneCard = true;
                        thisCard.AdjustAttackBonus(200);
                        thisEffect.AddAffectedByCard(thisCard);
                        UpdateEffectLogs(string.Format("Effect Applied to: [{0}] On Board ID: [{1}] Owned by [{2}] - Card is LIGHT Attribute and Owned by the effect.Controller. Increase its ATK by 200.", thisCard.Name, thisCard.OnBoardID, thisCard.Controller));
                    }
                }
            }

            if (!effectAppliedToAtLeastOneCard) { UpdateEffectLogs("No Cards were affected by it."); }

            //Step 3: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);
        }
        private void LightSymbol_ReactTo_NewMonsterUnderYourControl(Effect thisEffect, Card targetCard)
        {
            //If the monster summon is from the same.Controller AND...
            if (targetCard.Controller == thisEffect.Owner)
            {
                //is LIGHT Attribute...
                if (targetCard.CurrentAttribute == Attribute.LIGHT)
                {
                    //Give a 200 Attack Boost
                    targetCard.AdjustAttackBonus(200);
                    thisEffect.AddAffectedByCard(targetCard);
                    UpdateEffectLogs(string.Format("Effect Reacted: Summoned Card is LIGHT Attribute and Owned by the effect.Controller. Increase its ATK by 200."));
                }
            }
        }
        private void LightSymbol_ReactTo_AttributeChange(Effect thisEffect, Card targetCard)
        {
            //If the target card that changed its attributed was the Card affected by this Light Symbol's effect
            //then check if this Card needs a stat boost modification

            //if the target card WAS already in the Light Symbol's effect's affected by list and the target card is NOT longer LIGHT,
            //then reduce the attack boost and remove the target card from the affected by list
            if (thisEffect.AffectedByList.Contains(targetCard) && targetCard.CurrentAttribute != Attribute.LIGHT)
            {
                targetCard.AdjustAttackBonus(-200);
                thisEffect.RemoveAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is not longer a LIGHT attribute monster. The effects of Light Symbol do not apply to it anymore.");
            }
            //if the target card was NOT in the Light Symbol's effect's affected by list and the target card is NOW Light and both have the same.Controller
            //then increase the attack boost and add the target card to the affected by list
            else if (!thisEffect.AffectedByList.Contains(targetCard) && targetCard.CurrentAttribute == Attribute.LIGHT && thisEffect.Owner == targetCard.Controller)
            {
                targetCard.AdjustAttackBonus(200);
                thisEffect.AddAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is now a LIGHT attribute monster. Light Symbol's effect will now apply to this card.");
            }
            else
            {
                UpdateEffectLogs("Effect did not react.");
            }
        }
        #endregion

        #region "WATER Symbol"
        private void WaterSymbol_Activation(Effect thisEffect)
        {
            //EFFECT DESCRIPTION
            //Increase the ATK of all your WATER monsters on the board by 200.
            //During activation find all existing targets and give them the ATK increase.
            //This effect will react to all summons: if the summon is the.Controller's monster and its a WATER Attribute: affect it.            

            //Step 1: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterSummon = true;
            thisEffect.ReactsToAttributeChange = true;
            thisEffect.ReactsToMonsterControlChange = true;

            bool effectAppliedToAtLeastOneCard = false;
            //Step 2: Resolve the effect initial activation
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (thisCard.Controller == thisEffect.Owner)
                {
                    if (!thisCard.IsASymbol && thisCard.CurrentAttribute == Attribute.WATER)
                    {
                        effectAppliedToAtLeastOneCard = true;
                        thisCard.AdjustAttackBonus(200);
                        thisEffect.AddAffectedByCard(thisCard);
                        UpdateEffectLogs(string.Format("Effect Applied to: [{0}] On Board ID: [{1}] Owned by [{2}] - Card is WATER Attribute and Owned by the effect.Controller. Increase its ATK by 200.", thisCard.Name, thisCard.OnBoardID, thisCard.Controller));
                    }
                }
            }

            if (!effectAppliedToAtLeastOneCard) { UpdateEffectLogs("No Cards were affected by it."); }

            //Step 3: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);
        }
        private void WaterSymbol_ReactTo_NewMonsterUnderYourControl(Effect thisEffect, Card targetCard)
        {
            //If the monster summon is from the same.Controller AND...
            if (targetCard.Controller == thisEffect.Owner)
            {
                //is WATER Attribute...
                if (targetCard.CurrentAttribute == Attribute.WATER)
                {
                    //Give a 200 Attack Boost
                    targetCard.AdjustAttackBonus(200);
                    thisEffect.AddAffectedByCard(targetCard);
                    UpdateEffectLogs(string.Format("Effect Reacted: Summoned Card is DARK Attribute and Owned by the effect.Controller. Increase its ATK by 200."));
                }
            }
        }
        private void WaterSymbol_ReactTo_AttributeChange(Effect thisEffect, Card targetCard)
        {
            //If the target card that changed its attributed was the Card affected by this Water Symbol's effect
            //then check if this Card needs a stat boost modification

            //if the target card WAS already in the Water Symbol's effect's affected by list and the target card is NOT longer WATER,
            //then reduce the attack boost and remove the target card from the affected by list
            if (thisEffect.AffectedByList.Contains(targetCard) && targetCard.CurrentAttribute != Attribute.WATER)
            {
                targetCard.AdjustAttackBonus(-200);
                thisEffect.RemoveAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is not longer a WATER attribute monster. The effects of Water Symbol do not apply to it anymore.");
            }
            //if the target card was NOT in the Water Symbol's effect's affected by list and the target card is NOW WATER and both have the same.Controller
            //then increase the attack boost and add the target card to the affected by list
            else if (!thisEffect.AffectedByList.Contains(targetCard) && targetCard.CurrentAttribute == Attribute.WATER && thisEffect.Owner == targetCard.Controller)
            {
                targetCard.AdjustAttackBonus(200);
                thisEffect.AddAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is now a WATER attribute monster. Water Symbol's effect will now apply to this card.");
            }
            else
            {
                UpdateEffectLogs("Effect did not react.");
            }
        }
        #endregion

        #region "FIRE Symbol"
        private void FireSymbol_Activation(Effect thisEffect)
        {
            //EFFECT DESCRIPTION
            //Increase the ATK of all your FIRE monsters on the board by 200.
            //During activation find all existing targets and give them the ATK increase.
            //This effect will react to all summons: if the summon is the.Controller's monster and its a FIRE Attribute: affect it.            

            //Step 1: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterSummon = true;
            thisEffect.ReactsToAttributeChange = true;
            thisEffect.ReactsToMonsterControlChange = true;

            bool effectAppliedToAtLeastOneCard = false;
            //Step 2: Resolve the effect initial activation
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (thisCard.Controller == thisEffect.Owner)
                {
                    if (!thisCard.IsASymbol && thisCard.CurrentAttribute == Attribute.FIRE)
                    {
                        effectAppliedToAtLeastOneCard = true;
                        thisCard.AdjustAttackBonus(200);
                        thisEffect.AddAffectedByCard(thisCard);
                        UpdateEffectLogs(string.Format("Effect Applied to: [{0}] On Board ID: [{1}] Owned by [{2}] - Card is FIRE Attribute and Owned by the effect.Controller. Increase its ATK by 200.", thisCard.Name, thisCard.OnBoardID, thisCard.Controller));
                    }
                }
            }

            if (!effectAppliedToAtLeastOneCard) { UpdateEffectLogs("No Cards were affected by it."); }

            //Step 3: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);
        }
        private void FireSymbol_ReactTo_NewMonsterUnderYourControl(Effect thisEffect, Card targetCard)
        {
            //If the monster summon is from the same.Controller AND...
            if (targetCard.Controller == thisEffect.Owner)
            {
                //is FIRE Attribute...
                if (targetCard.CurrentAttribute == Attribute.FIRE)
                {
                    //Give a 200 Attack Boost
                    targetCard.AdjustAttackBonus(200);
                    thisEffect.AddAffectedByCard(targetCard);
                    UpdateEffectLogs(string.Format("Effect Reacted: Summoned Card is FIRE Attribute and Owned by the effect.Controller. Increase its ATK by 200."));
                }
            }
        }
        private void FireSymbol_ReactTo_AttributeChange(Effect thisEffect, Card targetCard)
        {
            //If the target card that changed its attributed was the Card affected by this Fire Symbol's effect
            //then check if this Card needs a stat boost modification

            //if the target card WAS already in the Fire Symbol's effect's affected by list and the target card is NOT longer FIRE,
            //then reduce the attack boost and remove the target card from the affected by list
            if (thisEffect.AffectedByList.Contains(targetCard) && targetCard.CurrentAttribute != Attribute.FIRE)
            {
                targetCard.AdjustAttackBonus(-200);
                thisEffect.RemoveAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is not longer a FIRE attribute monster. The effects of Fire Symbol do not apply to it anymore.");
            }
            //if the target card was NOT in the Fire Symbol's effect's affected by list and the target card is NOW WATER and both have the same.Controller
            //then increase the attack boost and add the target card to the affected by list
            else if (!thisEffect.AffectedByList.Contains(targetCard) && targetCard.CurrentAttribute == Attribute.FIRE && thisEffect.Owner == targetCard.Controller)
            {
                targetCard.AdjustAttackBonus(200);
                thisEffect.AddAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is now a FIRE attribute monster. Fire Symbol's effect will now apply to this card.");
            }
            else
            {
                UpdateEffectLogs("Effect did not react.");
            }
        }
        #endregion

        #region "EARTH Symbol"
        private void EarthSymbol_Activation(Effect thisEffect)
        {
            //EFFECT DESCRIPTION
            //Increase the ATK of all your FIRE monsters on the board by 200.
            //During activation find all existing targets and give them the ATK increase.
            //This effect will react to all summons: if the summon is the.Controller's monster and its a EARTH Attribute: affect it.            

            //Step 1: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterSummon = true;
            thisEffect.ReactsToAttributeChange = true;
            thisEffect.ReactsToMonsterControlChange = true;

            bool effectAppliedToAtLeastOneCard = false;
            //Step 2: Resolve the effect initial activation
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (thisCard.Controller == thisEffect.Owner)
                {
                    if (!thisCard.IsASymbol && thisCard.CurrentAttribute == Attribute.EARTH)
                    {
                        effectAppliedToAtLeastOneCard = true;
                        thisCard.AdjustAttackBonus(200);
                        thisEffect.AddAffectedByCard(thisCard);
                        UpdateEffectLogs(string.Format("Effect Applied to: [{0}] On Board ID: [{1}] Owned by [{2}] - Card is EARTH Attribute and Owned by the effect.Controller. Increase its ATK by 200.", thisCard.Name, thisCard.OnBoardID, thisCard.Controller));
                    }
                }
            }

            if (!effectAppliedToAtLeastOneCard) { UpdateEffectLogs("No Cards were affected by it."); }

            //Step 3: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);
        }
        private void EarthSymbol_ReactTo_NewMonsterUnderYourControl(Effect thisEffect, Card targetCard)
        {
            //If the monster summon is from the same.Controller AND...
            if (targetCard.Controller == thisEffect.Owner)
            {
                //is EARTH Attribute...
                if (targetCard.CurrentAttribute == Attribute.EARTH)
                {
                    //Give a 200 Attack Boost
                    targetCard.AdjustAttackBonus(200);
                    thisEffect.AddAffectedByCard(targetCard);
                    UpdateEffectLogs(string.Format("Effect Reacted: Summoned Card is EARTH Attribute and Owned by the effect.Controller. Increase its ATK by 200."));
                }
            }
        }
        private void EarthSymbol_ReactTo_AttributeChange(Effect thisEffect, Card targetCard)
        {
            //If the target card that changed its attributed was the Card affected by this Earth Symbol's effect
            //then check if this Card needs a stat boost modification

            //if the target card WAS already in the Earth Symbol's effect's affected by list and the target card is NOT longer EARTH,
            //then reduce the attack boost and remove the target card from the affected by list
            if (thisEffect.AffectedByList.Contains(targetCard) && targetCard.CurrentAttribute != Attribute.EARTH)
            {
                targetCard.AdjustAttackBonus(-200);
                thisEffect.RemoveAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is not longer an EARTH attribute monster. The effects of Earth Symbol do not apply to it anymore.");
            }
            //if the target card was NOT in the Earth Symbol's effect's affected by list and the target card is NOW EARTH and both have the same.Controller
            //then increase the attack boost and add the target card to the affected by list
            else if (!thisEffect.AffectedByList.Contains(targetCard) && targetCard.CurrentAttribute == Attribute.EARTH && thisEffect.Owner == targetCard.Controller)
            {
                targetCard.AdjustAttackBonus(200);
                thisEffect.AddAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is now an EARTH attribute monster. Earth Symbol's effect will now apply to this card.");
            }
            else
            {
                UpdateEffectLogs("Effect did not react.");
            }
        }
        #endregion

        #region "WIND Symbol"
        private void WindSymbol_Activation(Effect thisEffect)
        {
            //EFFECT DESCRIPTION
            //Increase the ATK of all your FIRE monsters on the board by 200.
            //During activation find all existing targets and give them the ATK increase.
            //This effect will react to all summons: if the summon is the.Controller's monster and its a WIND Attribute: affect it.            

            //Step 1: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterSummon = true;
            thisEffect.ReactsToAttributeChange = true;
            thisEffect.ReactsToMonsterControlChange = true;

            bool effectAppliedToAtLeastOneCard = false;
            //Step 2: Resolve the effect initial activation
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (thisCard.Controller == thisEffect.Owner)
                {
                    if (!thisCard.IsASymbol && thisCard.CurrentAttribute == Attribute.WIND)
                    {
                        effectAppliedToAtLeastOneCard = true;
                        thisCard.AdjustAttackBonus(200);
                        thisEffect.AddAffectedByCard(thisCard);
                        UpdateEffectLogs(string.Format("Effect Applied to: [{0}] On Board ID: [{1}] Owned by [{2}] - Card is WIND Attribute and Owned by the effect.Controller. Increase its ATK by 200.", thisCard.Name, thisCard.OnBoardID, thisCard.Controller));
                    }
                }
            }

            if (!effectAppliedToAtLeastOneCard) { UpdateEffectLogs("No Cards were affected by it."); }

            //Step 3: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);
        }
        private void WindSymbol_ReactTo_NewMonsterUnderYourControl(Effect thisEffect, Card targetCard)
        {
            //If the monster summon is from the same.Controller AND...
            if (targetCard.Controller == thisEffect.Owner)
            {
                //is WIND Attribute...
                if (targetCard.CurrentAttribute == Attribute.WIND)
                {
                    //Give a 200 Attack Boost
                    targetCard.AdjustAttackBonus(200);
                    thisEffect.AddAffectedByCard(targetCard);
                    UpdateEffectLogs(string.Format("Effect Reacted: Summoned Card is WIND Attribute and Owned by the effect.Controller. Increase its ATK by 200."));
                }
            }
        }
        private void WindSymbol_ReactTo_AttributeChange(Effect thisEffect, Card targetCard)
        {
            //If the target card that changed its attributed was the Card affected by this Wind Symbol's effect
            //then check if this Card needs a stat boost modification

            //if the target card WAS already in the Wind Symbol's effect's affected by list and the target card is NOT longer WIND,
            //then reduce the attack boost and remove the target card from the affected by list
            if (thisEffect.AffectedByList.Contains(targetCard) && targetCard.CurrentAttribute != Attribute.WIND)
            {
                targetCard.AdjustAttackBonus(-200);
                thisEffect.RemoveAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is not longer a WIND attribute monster. The effects of Wind Symbol do not apply to it anymore.");
            }
            //if the target card was NOT in the Wind Symbol's effect's affected by list and the target card is NOW WIND and both have the same.Controller
            //then increase the attack boost and add the target card to the affected by list
            else if (!thisEffect.AffectedByList.Contains(targetCard) && targetCard.CurrentAttribute == Attribute.EARTH && thisEffect.Owner == targetCard.Controller)
            {
                targetCard.AdjustAttackBonus(200);
                thisEffect.AddAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is now a WIND attribute monster. Wind Symbol's effect will now apply to this card.");
            }
            else
            {
                UpdateEffectLogs("Effect did not react.");
            }
        }
        #endregion

        #region Base Field Spells
        private void BaseFieldSpellActivation(Effect thisEffect, Tile.FieldTypeValue fieldType)
        {
            //EFFECT DESCRIPTION
            //Change the field type of all the active tile in the field activation area (7x7 tile grid)       

            //Step 1: Set the "Reaction To" flags
            //Field Spells are one and done and do not react to other events

            //Step 2: Resolve the effect initial activation
            List<Tile> AllFieldActivationTiles = _CurrentTileSelected.GetFieldSpellActivationTiles(false);
            List<Tile> FieldActivationTiles = _CurrentTileSelected.GetFieldSpellActivationTiles(true);

            //Highligh all the tiles in the field activation area
            foreach (Tile thisTile in AllFieldActivationTiles)
            {
                if (thisTile != null) { thisTile.HighlightTile(); }
            }
            WaitNSeconds(500);
            //then change the field type of all the active tiles
            foreach (Tile thisTile in FieldActivationTiles)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                thisTile.ChangeFieldType(fieldType);
                WaitNSeconds(300);
            }
            //finally. reset the all the tiles to clean up the UI
            foreach (Tile thisTile in AllFieldActivationTiles)
            {
                if (thisTile != null) { thisTile.ReloadTileUI(); }
            }

            UpdateEffectLogs(string.Format("Effect Applied, tiles on the board were change to [{0}] field type.", fieldType));

            //Step 3: Destroy the card once done
            DestroyCard(_CurrentTileSelected);

            //Step 4: Once activation resolution is over, return to the main phase
            EnterMainPhase();
        }
        private void Mountain_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Mountain);
        }
        private void Sogen_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Sogen);
        }
        private void Forest_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Forest);
        }
        private void Wasteland_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Wasteland);
        }
        private void Yami_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Yami);
        }
        private void Umi_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Umi);
        }
        private void Volcano_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Volcano);
        }
        private void Swamp_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Swamp);
        }
        private void Cyberworld_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Cyberworld);
        }
        private void Sanctuary_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Sanctuary);
        }
        private void Scrapyard_Activation(Effect thisEffect)
        {
            BaseFieldSpellActivation(thisEffect, Tile.FieldTypeValue.Scrapyard);
        }
        #endregion

        #region "M-Warrior #1"
        private void MWarrior1_OnSummonActivation(Effect thisEffect)
        {
            //Since this is a ON SUMMON EFFECT, display the Effect Panel for 2 secs then execute the effect
            DisplayOnSummonEffectPanel(thisEffect);

            //Set the "Reaction To" flags
            //This effect is a One-Time activation, does not reach to any event.

            //Hide the panel now to resolve the effect
            HideEffectMenuPanel();

            //EFFECT DESCRIPTION
            //Will increase the ATK of all your monsters on the board with the name "M-Warrior #2" by 500.
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (!thisCard.IsASymbol && !thisCard.IsDiscardted && thisCard.Name == "M-Warrior #2" && thisCard.Controller == thisEffect.Owner)
                {
                    thisCard.AdjustAttackBonus(500);
                    thisEffect.AddAffectedByCard(thisCard);
                    SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
                    UpdateEffectLogs(string.Format("Effect Applied to: [{0}] On Board ID: [{1}] Owned by [{2}] - Card's ATK increased by 500.", thisCard.Name, thisCard.OnBoardID, thisCard.Controller));
                }
            }

            //This monster does not have a continuous effect to active, move into the Main Phase
            EnterMainPhase();
        }
        private void MWarrior1_IgnitionActivation(Effect thisEffect)
        {
            //Hide the Effect Menu 
            HideEffectMenuPanel();

            //And Resolve the effect
            //EFFECT DESCRIPTION: Add 1 [DEF] to the owener's crest pool
            AdjustPlayerCrestCount(thisEffect.Owner, Crest.DEF, 1);
            UpdateEffectLogs("Effect Resolved: Added 1 [DEF] to.Controller's crest pool.");

            //NO more action needed, return to the Main Phase
            EnterMainPhase();
        }
        #endregion

        #region "M-Warrior #2"
        private void MWarrior2_OnSummonActivation(Effect thisEffect)
        {
            //Since this is a ON SUMMON EFFECT, display the Effect Panel for 2 secs then execute the effect
            DisplayOnSummonEffectPanel(thisEffect);

            //Set the "Reaction To" flags
            //This effect is a One-Time activation, does not reach to any event.

            //Hide the panel now to resolve the effect
            HideEffectMenuPanel();

            //EFFECT DESCRIPTION:Will increase the DEF of all your monsters on the board with the name "M-Warrior #1" by 500.
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (!thisCard.IsASymbol && !thisCard.IsDiscardted && thisCard.Name == "M-Warrior #1" && thisCard.Controller == thisEffect.Owner)
                {
                    thisCard.AdjustDefenseBonus(500);
                    thisEffect.AddAffectedByCard(thisCard);
                    SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
                    UpdateEffectLogs(string.Format("Effect Applied to: [{0}] On Board ID: [{1}] Owned by [{2}] - Card's DEF increased by 500.", thisCard.Name, thisCard.OnBoardID, thisCard.Controller));
                }
            }

            //This monster does not have a continuous effect to active, move into the Main Phase
            EnterMainPhase();
        }
        private void MWarrior2_IgnitionActivation(Effect thisEffect)
        {
            //Hide the Effect Menu 
            HideEffectMenuPanel();

            //And Resolve the effect
            //EFFECT DESCRIPTION: Add 1 [ATK] to the owener's crest pool
            AdjustPlayerCrestCount(thisEffect.Owner, Crest.ATK, 1);
            UpdateEffectLogs("Effect Resolved: Added 1 [ATK] to.Controller's crest pool.");

            //NO more action needed, return to the Main Phase
            EnterMainPhase();
        }
        #endregion

        #region Polymerization
        private void Polymerization_IgnitionActivation(Effect thisEffect)
        {
            //Destroy the Polymerization card
            _CurrentTileSelected.DestroyCard();

            //Now display the Fusion Selector Menu
            DisplayFusionSelectorMenu();

            //Change the game state and the player is now ready to pick the fusion monster
            _CurrentGameState = GameState.FusionSelectorMenu;

            void DisplayFusionSelectorMenu()
            {
                //Use the previously set list _FusionCardsReadyForFusion to display the available options to summon
                //Check the 3 fusion card slots in the deck

                //Check slot 1
                if (_FusionCardsReadyForFusion[0])
                {
                    int fusionCard1Id = TURNPLAYERDATA.Deck.GetFusionCardIDAtIndex(0);
                    if (UserPlayerColor == TURNPLAYER)
                    {
                        PicFusionOption1.Image = ImageServer.FullCardImage(fusionCard1Id.ToString());
                        btnFusionSummon1.Enabled = true;
                    }
                    else
                    {
                        PicFusionOption1.Image = ImageServer.FullCardImage("0");
                        btnFusionSummon1.Enabled = false;
                    }
                    PicFusionOption1.Visible = true;
                    btnFusionSummon1.Visible = true;
                }
                else
                {
                    PicFusionOption1.Visible = false;
                    btnFusionSummon1.Visible = false;
                }

                //Check slot 2
                if (_FusionCardsReadyForFusion[1])
                {
                    int fusionCard2Id = TURNPLAYERDATA.Deck.GetFusionCardIDAtIndex(1);
                    if (UserPlayerColor == TURNPLAYER)
                    {
                        PicFusionOption2.Image = ImageServer.FullCardImage(fusionCard2Id.ToString());
                        btnFusionSummon2.Enabled = true;
                    }
                    else
                    {
                        PicFusionOption2.Image = ImageServer.FullCardImage("0");
                        btnFusionSummon2.Enabled = false;
                    }
                    PicFusionOption2.Visible = true;
                    btnFusionSummon2.Visible = true;
                }
                else
                {
                    PicFusionOption2.Visible = false;
                    btnFusionSummon2.Visible = false;
                }

                //Check slot 3
                if (_FusionCardsReadyForFusion[2])
                {
                    int fusionCard3Id = TURNPLAYERDATA.Deck.GetFusionCardIDAtIndex(2);
                    if (UserPlayerColor == TURNPLAYER)
                    {
                        PicFusionOption3.Image = ImageServer.FullCardImage(fusionCard3Id.ToString());
                        btnFusionSummon3.Enabled = true;
                    }
                    else
                    {
                        PicFusionOption3.Image = ImageServer.FullCardImage("0");
                        btnFusionSummon3.Enabled = false;
                    }
                    PicFusionOption3.Visible = true;
                    btnFusionSummon3.Visible = true;
                }
                else
                {
                    PicFusionOption3.Visible = false;
                    btnFusionSummon3.Visible = false;
                }

                //Update the message for the opponent
                if (UserPlayerColor != TURNPLAYER)
                {
                    lblActionInstruction.Text = "Opponent is selecting a Fusion Monster to summon.";
                }

                PanelFusionMonsterSelector.Visible = true;
            }
        }
        #endregion

        #region Karbonala Warrior
        private void KarbonalaWarrior_ContinuousActivation(Effect thisEffect)
        {
            //Step 1: Since this is a CONTINUOUS, display the Effect Panel for 2 secs then execute the effect
            DisplayOnSummonContinuousEffectPanel(thisEffect);

            //Step 2: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterSummon = true;
            thisEffect.ReactsToMonsterDestroyed = true;

            //Step 3: Resolve the effect
            //EFFECT DESCRIPTION: Increase the ATK/DEF of this monster by 500 for each “M-Warrior #1” or “M-Warrior #2” on the board
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (!thisCard.IsDiscardted && (thisCard.Name == "M-Warrior #1" || thisCard.Name == "M-Warrior #2"))
                {
                    thisEffect.OriginCard.AdjustAttackBonus(500);
                    thisEffect.OriginCard.AdjustDefenseBonus(500);
                    UpdateEffectLogs(string.Format("Effect Applied: Card [{0}] On Board ID: [{1}] Owned by [{2}] is on the board. Increase origin's card's ATK/DEF by 500.", thisCard.Name, thisCard.OnBoardID, thisCard.Controller));
                }
            }

            //Step 4: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);

            //Step 5: Hide the Effect Menu panel and enter the Main Phase
            HideEffectMenuPanel();
            EnterMainPhase();
        }
        private void KarbonalaWarrior_ReactTo_MonsterSummon(Effect thisEffect, Card targetCard)
        {
            //If the monster summon is M-Warrior #1 or M-Warrior #2 regardless of.Controller
            if (targetCard.Name == "M-Warrior #1" || targetCard.Name == "M-Warrior #2")
            {
                //Give an extra boost to the origin monster
                thisEffect.OriginCard.AdjustAttackBonus(500);
                thisEffect.OriginCard.AdjustDefenseBonus(500);
                UpdateEffectLogs("Effect Reacted: Increase the origin card's ATK/DEF by 500.");
            }
        }
        private void KarbonalaWarrior_ReactTo_MonsterDestroyed(Effect thisEffect, Card targetCard)
        {
            //If the monster destroyed was M-Warrior #1 or M-Warrior #2 regardless of.Controller
            if (targetCard.Name == "M-Warrior #1" || targetCard.Name == "M-Warrior #2")
            {
                //reduce boost to the origin monster
                thisEffect.OriginCard.AdjustAttackBonus(-500);
                thisEffect.OriginCard.AdjustDefenseBonus(-500);
                UpdateEffectLogs(string.Format("Effect Reacted: Origin Card [{0}] with Board ID: [{1}] ATK/DEF boost decreased by 500.", thisEffect.OriginCard.Name, thisEffect.OriginCard.OnBoardID));
            }
        }
        private void KarbonalaWarrior_IgnitionActivation(Effect thisEffect)
        {
            //Hide the Effect Menu 
            HideEffectMenuPanel();

            //And Resolve the effect
            //EFFECT DESCRIPTION: Add 2 [ATK] and 2 [DEF] to the owener's crest pool
            AdjustPlayerCrestCount(thisEffect.Owner, Crest.ATK, 2);
            AdjustPlayerCrestCount(thisEffect.Owner, Crest.DEF, 2);
            UpdateEffectLogs("Effect Resolved: Added 1 [ATK] and 1 [DEF] to.Controller's crest pool.");

            //NO more action needed, return to the Main Phase
            EnterMainPhase();
        }
        #endregion

        #region Fire Kraken
        private void FireKraken_IgnitionActivation(Effect thisEffect)
        {
            //Hide the Effect Menu 
            HideEffectMenuPanel();

            //Set the "Reaction To" flags
            thisEffect.ReactsToAttributeChange = true;

            //And Resolve the effect
            //EFFECT DESCRIPTION: Target 1 opponent monster; change its attributo to FIRE until the end of this turn.

            //Generate the Target Candidate list
            _EffectTargetCandidates.Clear();
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (!thisCard.IsDiscardted && thisCard.Category == Category.Monster && thisCard.Controller == OPPONENTPLAYER)
                {
                    _EffectTargetCandidates.Add(thisCard.CurrentTile);
                }
            }
            UpdateEffectLogs(string.Format("Target candidates found: [{0}], player will be prompt to target a monster in the UI.", _EffectTargetCandidates.Count));

            //Flag the Effect Activation this turn
            thisEffect.OriginCard.MarkEffectUsedThisTurn();

            //The Target selection will handle takin the player to the next game state
            _CurrentPostTargetState = PostTargetState.FireKrakenEffect;
            InitializeEffectTargetSelection();
        }
        private void FireKraken_PostTargetEffect(Tile TargetTile)
        {
            //Restore the ref to the effect being used
            Effect thisEffect = _CardEffectToBeActivated;

            //Now apply the effect into the target
            Card targetCard = TargetTile.CardInPlace;

            //Resolve the effect: change the monster's attribute to FIRE
            targetCard.ChangeAttribute(Attribute.FIRE);
            thisEffect.AddAffectedByCard(targetCard);

            //Add this effect to the list of active effects
            _ActiveEffects.Add(thisEffect);

            //Update logs
            UpdateEffectLogs("Post Target Resolution: Target Card's Attribute was changed to FIRE.");

            //Before returning to the Main Phase, check if any other active effects on the board react to this attribute change
            ResolveEffectsWithAttributeChangeReactionTo(targetCard, thisEffect);

            //NO more action needed, return to the Main Phase
            EnterMainPhase();
        }
        private void FireKraken_RemoveEffect(Effect thisEffect)
        {
            //At the end of the turn remove the changes applied by this effect
            //Restore the Attribute of the affected by card to its original one
            Card theAffectedCard = thisEffect.AffectedByList[0];
            theAffectedCard.ResetAttribute();

            //Now remove this effect from the active effect list
            _ActiveEffects.Remove(thisEffect);

            //Update logs
            UpdateEffectLogs(string.Format("This effect removal resets the affected monster [{0}] with On Board ID [{1}]'s Attribute back to its original value [{2}].", theAffectedCard.Name, theAffectedCard.OnBoardID, theAffectedCard.CurrentAttribute));

            //This action modified a monster's attribute, check for other active effects that will react to it.
            ResolveEffectsWithAttributeChangeReactionTo(theAffectedCard, thisEffect);
        }
        private void FireKraken_ReactTo_AttributeChange(Effect thisEffect, Card targetCard, Effect modifierEffect)
        {
            //If the target card that changed its attributed was the Card affected by this Fire Kraken active effect
            //then remove this effect from the active effect list.
            //Whatever effect that changed the attribute of the target card will take over this effect.
            //Therefore, at the end of the turn, this effect will NOT reset the affected by monster's attribute back to its original.

            //This effect cannot react to itself...
            if (modifierEffect == thisEffect)
            {
                UpdateEffectLogs("Effect cannot react to its own effect resolution.");
            }
            else
            {
                if (thisEffect.AffectedByList.Contains(targetCard))
                {
                    UpdateEffectLogs("Reaction: This Card already had an Attribute Mod applied by this effect. The new effect that mod its Attribute again will override this effect. This effect will be remove from the active effects list.");
                    //DO NOT DO THIS: _ActiveEffects.Remove(thisEffect);
                    //We cannot remove effects from the ActiveEffect list while the reaction validations are taken place
                    //Add this effect to the _EffectsToBeRemoved... list so it can be done at the end.
                    _EffectsToBeRemovedByAttributeChangeReaction.Add(thisEffect);
                }
                else
                {
                    UpdateEffectLogs("Effect did not react.");
                }
            }
        }
        #endregion

        #region "Hitotsu-Me Giant"
        private void HitotsumeGiant_OnSummonActivation(Effect thisEffect)
        {
            //Since this is a ON SUMMON EFFECT, display the Effect Panel for 2 secs then execute the effect
            DisplayOnSummonEffectPanel(thisEffect);

            //EFFECT DESCRIPTION: Add 3 [ATK] to the controller's crest pool
            AdjustPlayerCrestCount(TURNPLAYER, Crest.ATK, 3);

            HideEffectMenuPanel();

            //At this point end the summoning phase
            EnterMainPhase();
        }
        #endregion

        #region "Thunder Dragon"
        private void ThunderDragon_Continuous(Effect thisEffect)
        {
            //Since this is a CONTINUOUS, display the Effect Panel for 2 secs then execute the effect
            DisplayOnSummonContinuousEffectPanel(thisEffect);

            //EFFECT DESCRIPTION
            //increase the DEF off all controller's owned Thunder-Type monsters by 500. (EXCEPT THE ORIGIN CARD)
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (thisCard.Controller == thisEffect.Owner)
                {
                    if (!thisCard.IsDiscardted && thisCard.Type == Type.Thunder && thisCard.OnBoardID != thisEffect.OriginCard.OnBoardID)
                    {
                        thisCard.AdjustDefenseBonus(500);
                        thisEffect.AddAffectedByCard(thisCard);
                        //Reload The Tile UI for the card affected
                        thisCard.ReloadTileUI();
                        UpdateEffectLogs(string.Format("Effect Applied: [{0}] | TO: [{1}] On Board ID: [{2}] Owned by [{3}]", thisEffect.ID, thisCard.Name, thisCard.OnBoardID, thisCard.Controller));
                    }
                }
            }

            HideEffectMenuPanel();

            //At this point end the summoning phase
            EnterMainPhase();
        }
        private void ThunderDragon_TryToApplyToNewCard(Effect thisEffect, Card targetCard)
        {
            if (targetCard.Controller == thisEffect.Owner)
            {
                if (targetCard.Type == Type.Thunder)
                {
                    targetCard.AdjustDefenseBonus(500);
                    thisEffect.AddAffectedByCard(targetCard);
                    UpdateEffectLogs(string.Format("Effect Applied: [{0}] | TO: [{1}] On Board ID: [{2}] Owned by [{3}] - Increased the card's DEF by 500.", thisEffect.ID, targetCard.Name, targetCard.OnBoardID, targetCard.Controller));
                }
            }
        }
        private void ThunderDragon_RemoveEffect(Effect thisEffect)
        {
            //Remove Effect Description:
            //DECREASE the DEF of all affected by monsters by 500
            foreach (Card thisCard in thisEffect.AffectedByList)
            {
                thisCard.AdjustDefenseBonus(-500);
                //Reload The Tile UI for the card affected
                thisCard.ReloadTileUI();
                UpdateEffectLogs(string.Format("Effect Removed: [{0}] | TO: [{1}] On Board ID: [{2}] Owned by [{3}]", thisEffect.ID, thisCard.Name, thisCard.OnBoardID, thisCard.Controller));
            }
        }
        #endregion
    }
}