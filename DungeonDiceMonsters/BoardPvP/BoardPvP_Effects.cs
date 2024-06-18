﻿//Joel Campos
//5/31/2024
//Partial Class for BoardPvP (Effect Execution Methods)

using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
                case Effect.EffectID.Polymerization_Ignition: Polymerization_IgnitionActivation(thisEffect); break;
                case Effect.EffectID.KarbonalaWarrior_Continuous: KarbonalaWarrior_ContinuousActivation(thisEffect); break;
                case Effect.EffectID.KarbonalaWarrior_Ignition: KarbonalaWarrior_IgnitionActivation(thisEffect); break;
                case Effect.EffectID.FireKraken_Ignition: FireKraken_IgnitionActivation(thisEffect); break;
                case Effect.EffectID.ChangeOfHeart_Ignition: ChangeOfHeart_IgnitionActivation(thisEffect); break;
                case Effect.EffectID.ThunderDragon_OnSummon: ThunderDragon_OnSummonActivation(thisEffect); break;
                case Effect.EffectID.TwinHeadedThunderDragon: TwinHeadedThunderDragon_ContinuousActivation(thisEffect); break;
                case Effect.EffectID.HitotsumeGiant_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.TRAP, 3); break;
                case Effect.EffectID.MasterExpert_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.MAG, 3); break;
                case Effect.EffectID.BigEye_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.MAG, 3); break;
                case Effect.EffectID.ThatWhichFeedsonLife_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.MOV, 3); break;
                case Effect.EffectID.TheThingintheCrater_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.TRAP, 3); break;
                case Effect.EffectID.Fireyarou_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.DEF, 3); break;
                case Effect.EffectID.GoddesswiththeThirdEye_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.MOV, 3); break;
                case Effect.EffectID.MoonEnvoy_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.ATK, 3); break;
                case Effect.EffectID.ArmaKnight_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.ATK, 3); break;
                case Effect.EffectID.WaterGirl_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.DEF, 3); break;
                case Effect.EffectID.WingedDragonGuardianoftheFortress2_OnSummmon: CrestBooster_OnSummonActivation(thisEffect, Crest.DEF, 3); break;
                case Effect.EffectID.KillerNeedle_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.MOV, 3); break;
                case Effect.EffectID.Hurricail_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.ATK, 7); break;
                case Effect.EffectID.LALALioon_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.MAG, 7); break;
                case Effect.EffectID.TheMeltingRedShadow_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.MOV, 7); break;
                case Effect.EffectID.TentaclePlant_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.TRAP, 7); break;
                case Effect.EffectID.Skelengel_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.TRAP, 7); break;
                case Effect.EffectID.WeatherControl_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.DEF, 7); break;
                case Effect.EffectID.PeopleRunningAround_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.MAG, 7); break;
                case Effect.EffectID.FlameDancer_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.ATK, 7); break;
                case Effect.EffectID.ArchfiendMarmotofNefariousness_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.MOV, 7); break;
                case Effect.EffectID.Haniwa_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.DEF, 7); break;
                case Effect.EffectID.BooKoo_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.TRAP, 7); break;
                case Effect.EffectID.CurtainoftheDarkOnes_OnSummon: CrestBooster_OnSummonActivation(thisEffect, Crest.DEF, 7); break;
                case Effect.EffectID.TrapHole_Trigger: TrapHole_TriggerActivation(thisEffect); break;
                case Effect.EffectID.AcidTrapHole_Trigger: AcidTrapHole_TriggerActivation(thisEffect); break;
                case Effect.EffectID.BanishingTrapHole_Trigger: BanishingTrapHole_TriggerActivation(thisEffect); break;
                case Effect.EffectID.DeepDarkTrapHole_Trigger: DeepDarkTrapHole_TriggerActivation(thisEffect); break;
                case Effect.EffectID.TreacherousTrapHole_Trigger: TreacherousTrapHole_TriggerActivation(thisEffect); break;
                case Effect.EffectID.BottomlessTrapHole_Trigger: BottomlessTrapHole_TriggerActivation(thisEffect); break;
                case Effect.EffectID.AdhesionTrapHole_Trigger: AdhesionTrapHole_TriggerActivation(thisEffect); break;
                case Effect.EffectID.Exodia_OnSummon: ExodiaTheForbiddenOne_OnSummonActivation(thisEffect); break;
                case Effect.EffectID.BlackLusterSoldier_Continuous: BlackLusterSoldier_ContinuousActivation(thisEffect); break;
                case Effect.EffectID.ShinatoKingOfAHigherPlane_Continuous: ShinatoKingOfAHigherPlane_ContinuousActivation(thisEffect); break;
                case Effect.EffectID.PetitMoth_Ingnition: PetitMoth_IgnitionActivation(thisEffect); break;
                case Effect.EffectID.CocoonofEvolution_Continuous: CocoonofEvolution_ContinuousActivation(thisEffect); break;
                case Effect.EffectID.CocoonofEvolution_Ignition: CocoonofEvolution_IgnitionActivation(thisEffect); break;
                case Effect.EffectID.LarvaeMoth_OnSummon: LarvaeMoth_OnSummonActivation(thisEffect); break;
                case Effect.EffectID.GreathMoth_OnSummon: GreatMoth_OnSummonActivation(thisEffect); break;
                default: throw new Exception(string.Format("Effect ID: [{0}] does not have an Activate Effect Function"));
            }
            UpdateEffectLogs("-----------------------------------------------------------------------------------------" + Environment.NewLine);
        }
        private void RemoveEffect(Effect thisEffect)
        {
            //then remove the effect from the Board
            switch (thisEffect.ID)
            {
                case Effect.EffectID.FireKraken_Ignition: FireKraken_RemoveEffect(thisEffect); break;
                case Effect.EffectID.ChangeOfHeart_Ignition: ChangeOfHeart_RemoveEffect(thisEffect); break;
                case Effect.EffectID.KarbonalaWarrior_Continuous: KarbonalaWarrior_RemoveEffect(thisEffect); break;
                case Effect.EffectID.TwinHeadedThunderDragon: TwinHeadedThunderDragon_RemoveEffect(thisEffect); break;
                case Effect.EffectID.BlackLusterSoldier_Continuous: BlackLusterSoldier_RemoveEffect(thisEffect); break;
                case Effect.EffectID.ShinatoKingOfAHigherPlane_Continuous: ShinatoKingOfAHigherPlane_RemoveEffect(thisEffect); break;
                case Effect.EffectID.CocoonofEvolution_Continuous: CocoonofEvolution_RemoveEffect(thisEffect); break;
                default: throw new Exception(string.Format("This effect id: [{0}] does not have a Remove Effect method assigned", thisEffect.ID));
            }
        }
        private void ReactivateEffect(Effect thisEffect)
        {
            UpdateEffectLogs(string.Format(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>Effect REactivation: [{0}] - Origin Card Board ID: [{1}].Controller: [{2}]", thisEffect.ID, thisEffect.OriginCard.OnBoardID, thisEffect.Owner));
            switch (thisEffect.ID)
            {                
                case Effect.EffectID.KarbonalaWarrior_Continuous: KarbonalaWarrior_ContinuousREActivation(thisEffect); break;
                case Effect.EffectID.TwinHeadedThunderDragon: TwinHeadedThunderDragon_ContinuousREActivation(thisEffect); break;
                case Effect.EffectID.BlackLusterSoldier_Continuous: BlackLusterSoldier_ContinuousREActivation(thisEffect); break;
                case Effect.EffectID.ShinatoKingOfAHigherPlane_Continuous: ShinatoKingOfAHigherPlane_ContinuousREActivation(thisEffect); break;
                case Effect.EffectID.CocoonofEvolution_Continuous: CocoonofEvolution_ContinuousREActivation(thisEffect); break;
                default: throw new Exception(string.Format("Effect ID: [{0}] does not have an REActivate Effect Function"));
            }
            UpdateEffectLogs("-----------------------------------------------------------------------------------------" + Environment.NewLine);
        }
        private void DisplayOnSummonEffectPanel(Effect thisEffect)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectMenu);
            //Load the menu with the On Summon effect
            ImageServer.ClearImage(PicEffectMenuCardImage);
            PicEffectMenuCardImage.Image = ImageServer.FullCardImage(thisEffect.OriginCard.CardID.ToString());
            lblEffectMenuTittle.Text = "Summon Effect";
            lblEffectMenuDescriiption.Text = thisEffect.OriginCard.OnSummonEffectText;
            //Hide the elements not needed.
            lblActivationRequirementOutput.Text = "";
            lblActivationRequirementOutput.Visible = false;
            PanelCost.Visible = false;
            btnActivate.Visible = false;
            btnEffectMenuCancel.Visible = false;
            //Wait 2 sec for players to reach the effect
            PanelEffectActivationMenu.Visible = true;
            WaitNSeconds(4000);
        }
        private void DisplayOnSummonEffectPanel(Effect thisEffect, string missingRequirements)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectMenu);
            //Load the menu with the On Summon effect
            ImageServer.ClearImage(PicEffectMenuCardImage);
            PicEffectMenuCardImage.Image = ImageServer.FullCardImage(thisEffect.OriginCard.CardID.ToString());
            lblEffectMenuTittle.Text = "Summon Effect";
            lblEffectMenuDescriiption.Text = thisEffect.OriginCard.OnSummonEffectText;
            //Hide the elements not needed.
            PanelCost.Visible = false;
            btnActivate.Visible = false;
            btnEffectMenuCancel.Visible = false;
            //Show the missing requirements
            lblActivationRequirementOutput.Text = missingRequirements;
            lblActivationRequirementOutput.Visible = true;
            //Wait 2 sec for players to reach the effect
            PanelEffectActivationMenu.Visible = true;
            WaitNSeconds(4000);
        }
        private void DisplayOnSummonContinuousEffectPanel(Effect thisEffect)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectMenu);
            ImageServer.ClearImage(PicEffectMenuCardImage);
            PicEffectMenuCardImage.Image = ImageServer.FullCardImage(thisEffect.OriginCard.CardID.ToString());
            lblEffectMenuTittle.Text = "Continuous Effect";
            lblEffectMenuDescriiption.Text = thisEffect.OriginCard.ContinuousEffectText;
            //Hide the elements not needed.
            lblActivationRequirementOutput.Text = "";
            lblActivationRequirementOutput.Visible = false;
            PanelCost.Visible = false;
            btnActivate.Visible = false;
            btnEffectMenuCancel.Visible = false;
            //Wait 4 sec for players to reach the effect
            PanelEffectActivationMenu.Visible = true;
            WaitNSeconds(4000);
            //Reduce the cost automatically
            AdjustPlayerCrestCount(thisEffect.Owner, thisEffect.CrestCost, thisEffect.CostAmount);
        }
        private void DisplayTriggerEffectPanel(Effect thisEffect)
        {
            SoundServer.PlaySoundEffect(SoundEffect.TriggerEffect);
            //Flip the face down card
            thisEffect.OriginCard.FlipFaceUp();

            ImageServer.ClearImage(PicEffectMenuCardImage);
            PicEffectMenuCardImage.Image = ImageServer.FullCardImage(thisEffect.OriginCard.CardID.ToString());
            lblEffectMenuTittle.Text = "Trigger Effect";
            lblEffectMenuDescriiption.Text = thisEffect.OriginCard.TriggerEffectText;
            //Hide the elements not needed.
            lblActivationRequirementOutput.Text = "";
            lblActivationRequirementOutput.Visible = false;
            //Cost
            ImageServer.ClearImage(PicCostCrest);
            PicCostCrest.Image = ImageServer.CrestIcon(thisEffect.CrestCost.ToString());
            lblCostAmount.Text = string.Format("x {0}", thisEffect.CostAmount);
            lblCostAmount.ForeColor = System.Drawing.Color.White;
            PanelCost.Visible = true;

            btnActivate.Visible = false;
            btnEffectMenuCancel.Visible = false;
            //Wait 4 sec for players to reach the effect
            PanelEffectActivationMenu.Visible = true;
            WaitNSeconds(4000);
            //Reduce the cost from the player's crest pool
            AdjustPlayerCrestCount(thisEffect.Owner, thisEffect.CrestCost, -thisEffect.CostAmount);
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
                case PostTargetState.ChangeOfHeartEffect: ChangeOfHeart_PostTargetEffect(TargetTile); break;
                case PostTargetState.ThunderDragonEffect: ThunderDragon_PostTargetEffect(TargetTile); break;
                case PostTargetState.GreatMothEffect: GreatMoth_PostTargetEffect(TargetTile); break;
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
            List<Effect> _EffectRemovalListByAttrChange = new List<Effect>();

            foreach (Effect thisActiveEffect in _ActiveEffects)
            {
                if (thisActiveEffect.ReactsToAttributeChange)
                {
                    UpdateEffectLogs(string.Format("Reaction Check for Effect: [{0}] Origin Card Board ID: [{1}]", thisActiveEffect.ID, thisActiveEffect.OriginCard.OnBoardID));
                    switch (thisActiveEffect.ID)
                    {
                        case Effect.EffectID.DARKSymbol: DarkSymbol_ReactTo_MonsterStatusChange(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.LIGHTSymbol: LightSymbol_ReactTo_MonsterStatusChange(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.WATERSymbol: WaterSymbol_ReactTo_MonsterStatusChange(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.FIRESymbol: FireSymbol_ReactTo_MonsterStatusChange(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.EARTHSymbol: EarthSymbol_ReactTo_MonsterStatusChange(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.WINDSymbol: WindSymbol_ReactTo_MonsterStatusChange(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.FireKraken_Ignition: FireKraken_ReactTo_AttributeChange(thisActiveEffect, targetCard, modifierEffect, _EffectRemovalListByAttrChange); break;
                        default: throw new Exception(string.Format("Effect ID: [{0}] does not have an [ReactTo_AttributeChange] Function", thisActiveEffect.ID));
                    }
                }
            }

            //Now safely remove any effects that need to be remove
            foreach (Effect thisEffectToRemove in _EffectRemovalListByAttrChange)
            {
                _ActiveEffects.Remove(thisEffectToRemove);
                UpdateEffectLogs(string.Format("The Effect [{0}] by card on board: [{1}] was remove from the active effect list.", thisEffectToRemove.ID, thisEffectToRemove.OriginCard.OnBoardID));
            }

            UpdateEffectLogs("-----------------------------------------------------------------------------------------" + Environment.NewLine);
        }
        private void ResolveEffectsWithMonsterControllerChangeReactionTo(Card targetCard, Effect modifierEffect)
        {
            UpdateEffectLogs(string.Format(">>>>>>>>>>>>>>>>>>>>>>>Card [{0}] with On Board Id [{1}] Controller was changed. Checking for active effects that react to it.", targetCard.Name, targetCard.OnBoardID));

            //NOTE REGARDING: "modifierEffect" argument
            //Some effect reactions are going to need to know which effect was the one that trigger the controller change event.
            //in order to validate if they react to it or not.
            //For example: Change of Heart will change the controller of a monster but that same action will trigger a reaction check on controller change
            //which Change of Heart WILL do. We do not want Change of Heart's effect to react to its own effect lol so I need to know which is the effect that
            //cause the modification, and if that effect is the same as the one being validating for reaction, it should NOT REACT.

            //Controller changes can make active effects to be override and thus removed from the active effects list
            //Use the following list to flag those effects and after all the effects in the active effect lists have been check for reaction,
            //remove them.
            //WE CANNOT REMOVE EFFECTS FROM THE _ActiveEffect LIST WHILE THE FOREACH LOOP IS ACTIVE

            List<Effect> _EffectRemovalListByMonsterControllerChange = new List<Effect>();
            foreach (Effect thisActiveEffect in _ActiveEffects)
            {
                if (thisActiveEffect.ReactsToMonsterControlChange)
                {
                    UpdateEffectLogs(string.Format("Reaction Check for Effect: [{0}] Origin Card Board ID: [{1}]", thisActiveEffect.ID, thisActiveEffect.OriginCard.OnBoardID));
                    switch (thisActiveEffect.ID)
                    {
                        case Effect.EffectID.DARKSymbol: DarkSymbol_ReactTo_MonsterStatusChange(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.LIGHTSymbol: LightSymbol_ReactTo_MonsterStatusChange(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.WATERSymbol: WaterSymbol_ReactTo_MonsterStatusChange(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.FIRESymbol: FireSymbol_ReactTo_MonsterStatusChange(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.EARTHSymbol: EarthSymbol_ReactTo_MonsterStatusChange(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.WINDSymbol: WindSymbol_ReactTo_MonsterStatusChange(thisActiveEffect, targetCard); break;
                        case Effect.EffectID.ChangeOfHeart_Ignition: ChangeOfHeart_ReactTo_MonsterControlChange(thisActiveEffect, targetCard, modifierEffect, _EffectRemovalListByMonsterControllerChange); break;
                        case Effect.EffectID.TwinHeadedThunderDragon: TwinHeadedThunderDragon_ReactTo_MonsterControlSwitch(thisActiveEffect, targetCard); break;
                        default: throw new Exception(string.Format("Effect ID: [{0}] does not have an [ReactTo_AttributeChange] Function", thisActiveEffect.ID));
                    }
                }
            }

            //Now safely remove any effects that need to be remove
            foreach (Effect thisEffectToRemove in _EffectRemovalListByMonsterControllerChange)
            {
                _ActiveEffects.Remove(thisEffectToRemove);
                UpdateEffectLogs(string.Format("The Effect [{0}] by card on board: [{1}] was remove from the active effect list.", thisEffectToRemove.ID, thisEffectToRemove.OriginCard.OnBoardID));
            }

            UpdateEffectLogs("-----------------------------------------------------------------------------------------" + Environment.NewLine);
        }
        private int ResolveEffectsWithDamageCalculationReactionTo(Card Attacker, Card Defender, Effect thisEffect, int ogDamageCalculated)
        {
            switch(thisEffect.ID)
            {
                case Effect.EffectID.BlackLusterSoldier_Continuous: return BlackLusterSoldier_ReactToBattleDamageCalculation(thisEffect, Attacker, ogDamageCalculated);
                default: throw new Exception(string.Format("Effect ID: [{0}] does not have an [ReactTo_BattleDamageCalculation] Function", thisEffect.ID));
            }

        }
        private void ResolverEffectsWithMonsterDestroyedByBattleReactionTo(Effect thisEffect, Card Attacker, Card Defender)
        {
            switch (thisEffect.ID)
            {
                case Effect.EffectID.ShinatoKingOfAHigherPlane_Continuous: ShinatoKingOfAHigherPlane_ReactToMonsterDestryedByBattle(thisEffect, Attacker, Defender); break;
                default: throw new Exception(string.Format("Effect ID: [{0}] does not have an [ReactTo_BattleDamageCalculation] Function", thisEffect.ID));
            }
        }
        private void ResolveEffectsWithEndPhaseReactionTo()
        {
            foreach (Effect thisActiveEffect in _ActiveEffects)
            {
                if (thisActiveEffect.ReactsToEndPhase)
                {
                    UpdateEffectLogs(string.Format("Reaction Check for Effect: [{0}] Origin Card Board ID: [{1}]", thisActiveEffect.ID, thisActiveEffect.OriginCard.OnBoardID));
                    switch (thisActiveEffect.ID)
                    {
                        case Effect.EffectID.CocoonofEvolution_Continuous: CocoonofEvolution_ReactTo_EndPhase(thisActiveEffect); break;
                        default: throw new Exception(string.Format("Effect ID: [{0}] does not have an [ReactTo_EndPhase] Function", thisActiveEffect.ID));
                    }
                }
            }
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
        private void DarkSymbol_ReactTo_MonsterStatusChange(Effect thisEffect, Card targetCard)
        {
            //This "React To" should apply to any Summon or Modification of ATTRIBUTE or CONTROLLER
            //All this reaction verification cares about is
            //1) The controller of the monster 2) The attribute of the monster 3) if the monster is already affected by this effect
            //Based on the above factors we can update whether or not we APPLY/REMOVE this effect to that card.

            //if the target card WAS already in the Dark Symbol's effect's "affected by" list and the target card is NOT DARK AND/OR controller by the effect's owner anymore,
            //then reduce the attack boost and remove the target card from the affected by list
            if (thisEffect.AffectedByList.Contains(targetCard) && (targetCard.CurrentAttribute != Attribute.DARK || targetCard.Controller != thisEffect.Owner))
            {
                targetCard.AdjustAttackBonus(-200);
                thisEffect.RemoveAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is not longer eligible for this effect. The effect of Dark Symbol will not apply to this card anymore. ATK boost was reduced by 200");
            }
            //if the target card was NOT in the Dark Symbol's effect's affected by list and the target card is DARK and controlled by the effect's owner
            //then increase the attack boost and add the target card to the affected by list
            else if (!thisEffect.AffectedByList.Contains(targetCard) && targetCard.CurrentAttribute == Attribute.DARK && thisEffect.Owner == targetCard.Controller)
            {
                targetCard.AdjustAttackBonus(200);
                thisEffect.AddAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is eligible for this effect. Dark Symbol's effect will now apply to this card. ATK boost was increased by 200.");
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
        private void LightSymbol_ReactTo_MonsterStatusChange(Effect thisEffect, Card targetCard)
        {
            //This "React To" should apply to any Summon or Modification of ATTRIBUTE or CONTROLLER
            //All this reaction verification cares about is
            //1) The controller of the monster 2) The attribute of the monster 3) if the monster is already affected by this effect
            //Based on the above factors we can update whether or not we APPLY/REMOVE this effect to that card.

            //if the target card WAS already in the Light Symbol's effect's "affected by" list and the target card is NOT LIGHT AND/OR controller by the effect's owner anymore,
            //then reduce the attack boost and remove the target card from the affected by list
            if (thisEffect.AffectedByList.Contains(targetCard) && (targetCard.CurrentAttribute != Attribute.LIGHT || targetCard.Controller != thisEffect.Owner))
            {
                targetCard.AdjustAttackBonus(-200);
                thisEffect.RemoveAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is not longer eligible for this effect. The effect of Light Symbol will not apply to this card anymore. ATK boost was reduced by 200");
            }
            //if the target card was NOT in the Light Symbol's effect's affected by list and the target card is Light and controlled by the effect's owner
            //then increase the attack boost and add the target card to the affected by list
            else if (!thisEffect.AffectedByList.Contains(targetCard) && targetCard.CurrentAttribute == Attribute.LIGHT && thisEffect.Owner == targetCard.Controller)
            {
                targetCard.AdjustAttackBonus(200);
                thisEffect.AddAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is eligible for this effect. Light Symbol's effect will now apply to this card. ATK boost was increased by 200.");
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
        private void WaterSymbol_ReactTo_MonsterStatusChange(Effect thisEffect, Card targetCard)
        {
            //This "React To" should apply to any Summon or Modification of ATTRIBUTE or CONTROLLER
            //All this reaction verification cares about is
            //1) The controller of the monster 2) The attribute of the monster 3) if the monster is already affected by this effect
            //Based on the above factors we can update whether or not we APPLY/REMOVE this effect to that card.

            //if the target card WAS already in the Water Symbol's effect's "affected by" list and the target card is NOT WATER AND/OR controller by the effect's owner anymore,
            //then reduce the attack boost and remove the target card from the affected by list
            if (thisEffect.AffectedByList.Contains(targetCard) && (targetCard.CurrentAttribute != Attribute.WATER || targetCard.Controller != thisEffect.Owner))
            {
                targetCard.AdjustAttackBonus(-200);
                thisEffect.RemoveAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is not longer eligible for this effect. The effect of Water Symbol will not apply to this card anymore. ATK boost was reduced by 200");
            }
            //if the target card was NOT in the Water Symbol's effect's affected by list and the target card is WATER and controlled by the effect's owner
            //then increase the attack boost and add the target card to the affected by list
            else if (!thisEffect.AffectedByList.Contains(targetCard) && targetCard.CurrentAttribute == Attribute.WATER && thisEffect.Owner == targetCard.Controller)
            {
                targetCard.AdjustAttackBonus(200);
                thisEffect.AddAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is eligible for this effect. Water Symbol's effect will now apply to this card. ATK boost was increased by 200.");
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
        private void FireSymbol_ReactTo_MonsterStatusChange(Effect thisEffect, Card targetCard)
        {
            //This "React To" should apply to any Summon or Modification of ATTRIBUTE or CONTROLLER
            //All this reaction verification cares about is
            //1) The controller of the monster 2) The attribute of the monster 3) if the monster is already affected by this effect
            //Based on the above factors we can update whether or not we APPLY/REMOVE this effect to that card.

            //if the target card WAS already in the Fire Symbol's effect's "affected by" list and the target card is NOT FIRE AND/OR controller by the effect's owner anymore,
            //then reduce the attack boost and remove the target card from the affected by list
            if (thisEffect.AffectedByList.Contains(targetCard) && (targetCard.CurrentAttribute != Attribute.FIRE || targetCard.Controller != thisEffect.Owner))
            {
                targetCard.AdjustAttackBonus(-200);
                thisEffect.RemoveAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is not longer eligible for this effect. The effect of Fire Symbol will not apply to this card anymore. ATK boost was reduced by 200");
            }
            //if the target card was NOT in the Fire Symbol's effect's affected by list and the target card is Fire and controlled by the effect's owner
            //then increase the attack boost and add the target card to the affected by list
            else if (!thisEffect.AffectedByList.Contains(targetCard) && targetCard.CurrentAttribute == Attribute.FIRE && thisEffect.Owner == targetCard.Controller)
            {
                targetCard.AdjustAttackBonus(200);
                thisEffect.AddAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is eligible for this effect. Fire Symbol's effect will now apply to this card. ATK boost was increased by 200.");
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
        private void EarthSymbol_ReactTo_MonsterStatusChange(Effect thisEffect, Card targetCard)
        {
            //This "React To" should apply to any Summon or Modification of ATTRIBUTE or CONTROLLER
            //All this reaction verification cares about is
            //1) The controller of the monster 2) The attribute of the monster 3) if the monster is already affected by this effect
            //Based on the above factors we can update whether or not we APPLY/REMOVE this effect to that card.

            //if the target card WAS already in the Earth Symbol's effect's "affected by" list and the target card is NOT EARTH AND/OR controller by the effect's owner anymore,
            //then reduce the attack boost and remove the target card from the affected by list
            if (thisEffect.AffectedByList.Contains(targetCard) && (targetCard.CurrentAttribute != Attribute.EARTH || targetCard.Controller != thisEffect.Owner))
            {
                targetCard.AdjustAttackBonus(-200);
                thisEffect.RemoveAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is not longer eligible for this effect. The effect of Earth Symbol will not apply to this card anymore. ATK boost was reduced by 200");
            }
            //if the target card was NOT in the Earth Symbol's effect's affected by list and the target card is EARTH and controlled by the effect's owner
            //then increase the attack boost and add the target card to the affected by list
            else if (!thisEffect.AffectedByList.Contains(targetCard) && targetCard.CurrentAttribute == Attribute.EARTH && thisEffect.Owner == targetCard.Controller)
            {
                targetCard.AdjustAttackBonus(200);
                thisEffect.AddAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is eligible for this effect. Earth Symbol's effect will now apply to this card. ATK boost was increased by 200.");
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
        private void WindSymbol_ReactTo_MonsterStatusChange(Effect thisEffect, Card targetCard)
        {
            //This "React To" should apply to any Summon or Modification of ATTRIBUTE or CONTROLLER
            //All this reaction verification cares about is
            //1) The controller of the monster 2) The attribute of the monster 3) if the monster is already affected by this effect
            //Based on the above factors we can update whether or not we APPLY/REMOVE this effect to that card.

            //if the target card WAS already in the Wind Symbol's effect's "affected by" list and the target card is NOT WIND AND/OR controller by the effect's owner anymore,
            //then reduce the attack boost and remove the target card from the affected by list
            if (thisEffect.AffectedByList.Contains(targetCard) && (targetCard.CurrentAttribute != Attribute.WIND || targetCard.Controller != thisEffect.Owner))
            {
                targetCard.AdjustAttackBonus(-200);
                thisEffect.RemoveAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is not longer eligible for this effect. The effect of Wind Symbol will not apply to this card anymore. ATK boost was reduced by 200");
            }
            //if the target card was NOT in the Wind Symbol's effect's affected by list and the target card is WIND and controlled by the effect's owner
            //then increase the attack boost and add the target card to the affected by list
            else if (!thisEffect.AffectedByList.Contains(targetCard) && targetCard.CurrentAttribute == Attribute.WIND && thisEffect.Owner == targetCard.Controller)
            {
                targetCard.AdjustAttackBonus(200);
                thisEffect.AddAffectedByCard(targetCard);
                UpdateEffectLogs("TARGET CARD is eligible for this effect. Wind Symbol's effect will now apply to this card. ATK boost was increased by 200.");
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

            //Enter Summon phase 3
            SummonMonster_Phase3(thisEffect.OriginCard);
        }
        private void MWarrior1_IgnitionActivation(Effect thisEffect)
        {
            //Hide the Effect Menu 
            HideEffectMenuPanel();

            //And Resolve the effect
            //EFFECT DESCRIPTION: Add 1 [DEF] to the owener's crest pool
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
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

            //Enter Summon phase 3
            SummonMonster_Phase3(thisEffect.OriginCard);
        }
        private void MWarrior2_IgnitionActivation(Effect thisEffect)
        {
            //Hide the Effect Menu 
            HideEffectMenuPanel();

            //And Resolve the effect
            //EFFECT DESCRIPTION: Add 1 [ATK] to the owener's crest pool
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            AdjustPlayerCrestCount(thisEffect.Owner, Crest.ATK, 1);
            UpdateEffectLogs("Effect Resolved: Added 1 [ATK] to.Controller's crest pool.");

            //NO more action needed, return to the Main Phase
            EnterMainPhase();
        }
        #endregion

        #region Polymerization
        private void Polymerization_IgnitionActivation(Effect thisEffect)
        {           
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
                        ImageServer.ClearImage(PicFusionOption1);
                        PicFusionOption1.Image = ImageServer.FullCardImage(fusionCard1Id.ToString());
                        btnFusionSummon1.Enabled = true;
                    }
                    else
                    {
                        ImageServer.ClearImage(PicFusionOption1);
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
                        ImageServer.ClearImage(PicFusionOption2);
                        PicFusionOption2.Image = ImageServer.FullCardImage(fusionCard2Id.ToString());
                        btnFusionSummon2.Enabled = true;
                    }
                    else
                    {
                        ImageServer.ClearImage(PicFusionOption2);
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
                        ImageServer.ClearImage(PicFusionOption3);
                        PicFusionOption3.Image = ImageServer.FullCardImage(fusionCard3Id.ToString());
                        btnFusionSummon3.Enabled = true;
                    }
                    else
                    {
                        ImageServer.ClearImage(PicFusionOption3);
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
            //Step 1: Since this is a CONTINUOUS, display the Effect Panel for 4 secs then execute the effect
            DisplayOnSummonContinuousEffectPanel(thisEffect);

            //Step 2: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterSummon = true;
            thisEffect.ReactsToMonsterDestroyed = true;

            //Step 3: Resolve the effect
            //EFFECT DESCRIPTION: Increase the ATK/DEF of this monster by 500 for each “M-Warrior #1” or “M-Warrior #2” on the board
            thisEffect.CustomInt1 = 0; //CustomInt1 will keep track how many bonuses this monster has by this effect
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (!thisCard.IsDiscardted && (thisCard.Name == "M-Warrior #1" || thisCard.Name == "M-Warrior #2"))
                {
                    thisEffect.OriginCard.AdjustAttackBonus(500);
                    thisEffect.OriginCard.AdjustDefenseBonus(500);
                    thisEffect.CustomInt1++;
                    UpdateEffectLogs(string.Format("Effect Applied: Card [{0}] On Board ID: [{1}] Owned by [{2}] is on the board. Increase origin's card's ATK/DEF by 500.", thisCard.Name, thisCard.OnBoardID, thisCard.Controller));
                }
            }

            //Step 4: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);

            //Step 5: Hide the Effect Menu panel
            HideEffectMenuPanel();
            //Enter Summon phase 4
            SummonMonster_Phase4(thisEffect.OriginCard);
        }
        private void KarbonalaWarrior_ContinuousREActivation(Effect thisEffect)
        {
            //Step 1: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterSummon = true;
            thisEffect.ReactsToMonsterDestroyed = true;

            //Step 2: Resolve the effect
            //EFFECT DESCRIPTION: Increase the ATK/DEF of this monster by 500 for each “M-Warrior #1” or “M-Warrior #2” on the board
            thisEffect.CustomInt1 = 0; //CustomInt1 will keep track how many bonuses this monster has by this effect
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (!thisCard.IsDiscardted && (thisCard.Name == "M-Warrior #1" || thisCard.Name == "M-Warrior #2"))
                {
                    thisEffect.OriginCard.AdjustAttackBonus(500);
                    thisEffect.OriginCard.AdjustDefenseBonus(500);
                    thisEffect.CustomInt1++;
                    UpdateEffectLogs(string.Format("Effect Applied: Card [{0}] On Board ID: [{1}] Owned by [{2}] is on the board. Increase origin's card's ATK/DEF by 500.", thisCard.Name, thisCard.OnBoardID, thisCard.Controller));
                }
            }

            //Step 3: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);
        }
        private void KarbonalaWarrior_ReactTo_MonsterSummon(Effect thisEffect, Card targetCard)
        {
            //If the monster summon is M-Warrior #1 or M-Warrior #2 regardless of.Controller
            if (targetCard.Name == "M-Warrior #1" || targetCard.Name == "M-Warrior #2")
            {
                //Give an extra boost to the origin monster
                thisEffect.OriginCard.AdjustAttackBonus(500);
                thisEffect.OriginCard.AdjustDefenseBonus(500);
                thisEffect.CustomInt1++;
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
                thisEffect.CustomInt1--;
                UpdateEffectLogs(string.Format("Effect Reacted: Origin Card [{0}] with Board ID: [{1}] ATK/DEF boost decreased by 500.", thisEffect.OriginCard.Name, thisEffect.OriginCard.OnBoardID));
            }
        }
        private void KarbonalaWarrior_RemoveEffect(Effect thisEffect)
        {
            //Remove this effect by remove the ATK/DEF bonuses from the origin card that were added by this effect
            int boostToBeReduced = thisEffect.CustomInt1 * 500;
            thisEffect.OriginCard.AdjustAttackBonus(-boostToBeReduced);
            thisEffect.OriginCard.AdjustDefenseBonus(-boostToBeReduced);
            if (!thisEffect.OriginCard.IsDiscardted) { thisEffect.OriginCard.ReloadTileUI(); }

            //Now remove the effect from the active list
            _ActiveEffects.Remove(thisEffect);

            //Update logs
            UpdateEffectLogs(string.Format("This effect was removed from the active effect list. Origin Card's ATK/DEF bonus was reduced by [{0}]", boostToBeReduced));
        }
        private void KarbonalaWarrior_IgnitionActivation(Effect thisEffect)
        {
            //Hide the Effect Menu 
            HideEffectMenuPanel();

            //And Resolve the effect
            //EFFECT DESCRIPTION: Add 2 [ATK] and 2 [DEF] to the owener's crest pool
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
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
                if (!thisCard.IsDiscardted && thisCard.Category == Category.Monster && thisCard.Controller == OPPONENTPLAYER && thisCard.CanBeTarget)
                {
                    _EffectTargetCandidates.Add(thisCard.CurrentTile);
                }
            }
            UpdateEffectLogs(string.Format("Target candidates found: [{0}], player will be prompt to target a monster in the UI.", _EffectTargetCandidates.Count));

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
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
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

            //Now remove the effect from the active list
            _ActiveEffects.Remove(thisEffect);

            //Update logs
            UpdateEffectLogs(string.Format("This effect removal resets the affected monster [{0}] with On Board ID [{1}]'s Attribute back to its original value [{2}].", theAffectedCard.Name, theAffectedCard.OnBoardID, theAffectedCard.CurrentAttribute));

            //This action modified a monster's attribute, check for other active effects that will react to it.
            ResolveEffectsWithAttributeChangeReactionTo(theAffectedCard, thisEffect);
        }
        private void FireKraken_ReactTo_AttributeChange(Effect thisEffect, Card targetCard, Effect modifierEffect, List<Effect> _removalList)
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
                    _removalList.Add(thisEffect);
                }
                else
                {
                    UpdateEffectLogs("Effect did not react.");
                }
            }
        }
        #endregion

        #region Change of Heart
        private void ChangeOfHeart_IgnitionActivation(Effect thisEffect)
        {
            //Hide the Effect Menu 
            HideEffectMenuPanel();
           
            //Set the "Reaction To" flags
            thisEffect.ReactsToMonsterControlChange = true;

            //And Resolve the effect
            //EFFECT DESCRIPTION: Target 1 opponent monster; take control of it until the end of this turn

            //Generate the Target Candidate list
            _EffectTargetCandidates.Clear();
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (!thisCard.IsDiscardted && thisCard.Category == Category.Monster && thisCard.Controller == OPPONENTPLAYER && thisCard.CanBeTarget)
                {
                    _EffectTargetCandidates.Add(thisCard.CurrentTile);
                }
            }
            UpdateEffectLogs(string.Format("Target candidates found: [{0}], player will be prompt to target a monster in the UI.", _EffectTargetCandidates.Count));

            //The Target selection will handle takin the player to the next game state
            _CurrentPostTargetState = PostTargetState.ChangeOfHeartEffect;
            InitializeEffectTargetSelection();
        }
        private void ChangeOfHeart_PostTargetEffect(Tile TargetTile)
        {
            //Restore the ref to the effect being used
            Effect thisEffect = _CardEffectToBeActivated;

            //Now apply the effect into the target
            Card targetCard = TargetTile.CardInPlace;

            //Resolve the effect: change the monster's controllers to the TURNPLAYER
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            targetCard.SwitchController();
            thisEffect.AddAffectedByCard(targetCard);

            //Add this effect to the list of active effects
            _ActiveEffects.Add(thisEffect);

            //Now we can discard the card
            DestroyCard(_EffectOriginTile);

            //Update logs
            UpdateEffectLogs(string.Format("Post Target Resolution: Target Card's controller has been switched. New controller: [{0}]", targetCard.Controller));

            //Before returning to the Main Phase, check if any other active effects on the board react to this attribute change
            ResolveEffectsWithMonsterControllerChangeReactionTo(targetCard, thisEffect);

            //NO more action needed, return to the Main Phase
            EnterMainPhase();
        }
        private void ChangeOfHeart_RemoveEffect(Effect thisEffect)
        {
            //At the end of the turn remove the changes applied by this effect
            //Switch the Control back to the opponent
            Card theAffectedCard = thisEffect.AffectedByList[0];
            theAffectedCard.SwitchController();

            //Now remove the effect from the active list
            _ActiveEffects.Remove(thisEffect);

            //Update logs
            UpdateEffectLogs(string.Format("This effect removal switched the controller of Card [{0}] with On Board ID [{1}] back to the opponent: [{2}].", theAffectedCard.Name, theAffectedCard.OnBoardID, theAffectedCard.Controller));

            //This action switched a monster's controller, check for other active effects that will react to it.
            ResolveEffectsWithMonsterControllerChangeReactionTo(theAffectedCard, thisEffect);
        }
        private void ChangeOfHeart_ReactTo_MonsterControlChange(Effect thisEffect, Card targetCard, Effect modifierEffect, List<Effect> _removalList)
        {
            //If the target card that had its controlled change was the Card affected by this Change of Heart active effect
            //then remove this effect from the active effect list.
            //Whatever effect that changed the controller of the target card will take over this effect.
            //Therefore, at the end of the turn, this effect will switch the control of this monster back to your opponent. 

            //This effect cannot react to itself...
            if (modifierEffect == thisEffect)
            {
                UpdateEffectLogs("Effect cannot react to its own effect resolution.");
            }
            else
            {
                if (thisEffect.AffectedByList.Contains(targetCard))
                {
                    UpdateEffectLogs("Reaction: This Card already had a Controllerd Swtich applied by this effect. The new effect that switched its controller again will override this effect. This effect will be remove from the active effects list.");
                    //DO NOT DO THIS: _ActiveEffects.Remove(thisEffect);
                    //We cannot remove effects from the ActiveEffect list while the reaction validations are taken place
                    //Add this effect to the _EffectsToBeRemoved... list so it can be done at the end.
                    _removalList.Add(thisEffect);
                }
                else
                {
                    UpdateEffectLogs("Effect did not react.");
                }
            }
        }
        #endregion      

        #region "Thunder Dragon"
        private void ThunderDragon_OnSummonActivation(Effect thisEffect)
        {
            //ON SUMMON EFFECT will not activate if the monster was Transform Summoned (Transformed into)
            if(thisEffect.OriginCard.WasTransformedInto)
            {
                //Effect wont be activated, display with Effect Menu panel with the missing requirements
                DisplayOnSummonEffectPanel(thisEffect, "Cannot activate when monster was transformed into. Effect wont activate.");
                UpdateEffectLogs("Monster was transformed into, effect wont activate");
                HideEffectMenuPanel();
                //Enter Summon phase 3
                SummonMonster_Phase3(thisEffect.OriginCard);
            }
            else
            {
                //This ON SUMMON EFFECT needs a valid target candidate
                //We need to determine if there is a candidate or not first
                _EffectTargetCandidates.Clear();
                foreach (Card thisCard in _CardsOnBoard)
                {
                    if (!thisCard.IsASymbol && !thisCard.IsDiscardted && thisCard.Controller == thisEffect.Owner &&
                        (thisCard.Type == Type.Thunder || thisCard.Type == Type.Dragon) && thisCard.OriginalATK <= 1500)
                    {
                        //This is a candidate
                        _EffectTargetCandidates.Add(thisCard.CurrentTile);
                    }
                }
                UpdateEffectLogs(string.Format("Target candidates found: [{0}]", _EffectTargetCandidates.Count));

                if (_EffectTargetCandidates.Count > 0)
                {
                    //Proceed with the effect activation path
                    DisplayOnSummonEffectPanel(thisEffect);
                    HideEffectMenuPanel();

                    //Set the "Reaction To" flags
                    //This effect is a One-Time activation, does not reach to any event.

                    //The Target selection will handle takin the player to the next game state
                    _CurrentPostTargetState = PostTargetState.ThunderDragonEffect;
                    InitializeEffectTargetSelection();
                }
                else
                {
                    //Effect wont be activated, display with Effect Menu panel with the missing requirements
                    DisplayOnSummonEffectPanel(thisEffect, "No valid targets for activation. Effect wont activate.");
                    UpdateEffectLogs("There are not proper effect targets for this this effect. Effect will not activate.");
                    HideEffectMenuPanel();
                    //Enter Phase 3 of the summon sequence
                    SummonMonster_Phase3(thisEffect.OriginCard);
                }
            }
        }
        private void ThunderDragon_PostTargetEffect(Tile TargetTile)
        {
            //Now apply the effect into the target

            //Resolve the effect: Transform target into "Thunder Dragon" Card ID: 31786629
            TransformMonster(TargetTile, 31786629);

            //Enter Summon phase 3
            SummonMonster_Phase3(CardsBeingSummoned[0]);
        }
        #endregion

        #region "Twin-Headed Thunder Dragon"
        private void TwinHeadedThunderDragon_ContinuousActivation(Effect thisEffect)
        {
            //Step 1: Since this is a CONTINUOUS, display the Effect Panel then execute the effect
            DisplayOnSummonContinuousEffectPanel(thisEffect);

            //Step 2: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterSummon = true;
            thisEffect.ReactsToMonsterDestroyed = true;
            thisEffect.ReactsToMonsterControlChange = true;

            //Step 3: Resolve the effect
            //EFFECT DESCRIPTION: Increase Attack Range Bonus +1 for each "Twin-Headed Thunder Dragon" from the same controller.
            thisEffect.CustomInt1 = 0;
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (!thisCard.IsDiscardted && thisCard.Name == "Twin-Headed Thunder Dragon" && thisCard != thisEffect.OriginCard && 
                    thisCard.Controller == thisEffect.Owner)
                {
                    thisEffect.OriginCard.AdjustAttackRangeBonus(1);
                    thisEffect.CustomInt1++;
                    UpdateEffectLogs(string.Format("Effect Applied: Card [{0}] On Board ID: [{1}] Owned by [{2}] is on the board. Increase origin's card's Attack Range +1", thisCard.Name, thisCard.OnBoardID, thisCard.Controller));
                }
            }

            //Step 4: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);

            //Step 5: Hide the Effect Menu panel and Enter Summon phase 4
            HideEffectMenuPanel();
            SummonMonster_Phase4(thisEffect.OriginCard);
        }
        private void TwinHeadedThunderDragon_ContinuousREActivation(Effect thisEffect)
        {
            //Step 2: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterSummon = true;
            thisEffect.ReactsToMonsterDestroyed = true;
            thisEffect.ReactsToMonsterControlChange = true;

            //Step 3: Resolve the effect
            //EFFECT DESCRIPTION: Increase Attack Range Bonus +1 for each "Twin-Headed Thunder Dragon" from the same controller.
            thisEffect.CustomInt1 = 0;
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (!thisCard.IsDiscardted && thisCard.Name == "Twin-Headed Thunder Dragon" && thisCard != thisEffect.OriginCard &&
                    thisCard.Controller == thisEffect.Owner)
                {
                    thisEffect.OriginCard.AdjustAttackRangeBonus(1);
                    thisEffect.CustomInt1++;
                    UpdateEffectLogs(string.Format("Effect Applied: Card [{0}] On Board ID: [{1}] Owned by [{2}] is on the board. Increase origin's card's Attack Range +1", thisCard.Name, thisCard.OnBoardID, thisCard.Controller));
                }
            }

            //Step 4: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);
        }
        private void TwinHeadedThunderDragon_ReactTo_MonsterSummon(Effect thisEffect, Card targetCard)
        {
            //If the monster summoned is "Twin-Headed Thunder Dragon" controlled by the same player
            if (targetCard.Name == "Twin-Headed Thunder Dragon" && targetCard.Controller == thisEffect.Owner)
            {
                //Increase the origin card's attack range bonus +1
                thisEffect.OriginCard.AdjustAttackRangeBonus(1);
                thisEffect.CustomInt1++;
                UpdateEffectLogs("Effect Reacted: Increase the origin card's attack range +1.");
            }
        }
        private void TwinHeadedThunderDragon_ReactTo_MonsterDestroyed(Effect thisEffect, Card targetCard)
        {
            //If the monster destroyed was "Twin-Headed Thunder Dragon" controlled by the same player
            if (targetCard.Name == "Twin-Headed Thunder Dragon" && targetCard.Controller == thisEffect.Owner)
            {
                //Reduce the origin card's attack range bonus -1
                thisEffect.OriginCard.AdjustAttackRangeBonus(-1);
                thisEffect.CustomInt1--;
                UpdateEffectLogs("Effect Reacted: Reduced the origin card's attack range -1.");
            }
        }
        private void TwinHeadedThunderDragon_ReactTo_MonsterControlSwitch(Effect thisEffect, Card targetCard)
        {
            //This effect can react to the controller change of its own origin card
            if(targetCard == thisEffect.OriginCard)
            {
                //if this is the case, recalculate the Attack Range bonus by reseting the Attack Range Bonus
                //provided by this effect previously using the "CustonInt1" field
                int currentAttackRangeBonus = thisEffect.CustomInt1;
                targetCard.AdjustAttackRangeBonus(-currentAttackRangeBonus);
                thisEffect.CustomInt1 = 0;

                UpdateEffectLogs("Effect Reacted: This effect's origin card changed controller. This card needs to reset its effect based on the new controller's status.");
                UpdateEffectLogs(string.Format("The Attack Range bonus of this Card by this effect was [{0}], this will be reverted and recalculated.", currentAttackRangeBonus));

                //Now that this effect has "removed" itself from when this target monster was controlled
                //by the opposite player, recalculate the Attack Range bonus as it was activated for the first time.
                foreach (Card thisCard in _CardsOnBoard)
                {
                    if (!thisCard.IsDiscardted && thisCard.Name == "Twin-Headed Thunder Dragon" && thisCard != thisEffect.OriginCard &&
                        thisCard.Controller == thisEffect.Owner)
                    {
                        thisEffect.OriginCard.AdjustAttackRangeBonus(1);
                        thisEffect.CustomInt1++;
                        UpdateEffectLogs(string.Format("Effect Applied: Card [{0}] On Board ID: [{1}] Owned by [{2}] is on the board. Increase origin's card's Attack Range +1", thisCard.Name, thisCard.OnBoardID, thisCard.Controller));
                    }
                }
            }
            else
            {
                //If the monster who had its controller changed is "Twin-Headed Thunder Dragon"
                if (targetCard.Name == "Twin-Headed Thunder Dragon")
                {
                    //Then check who is the new controller

                    //if controller by the same player
                    if (targetCard.Controller == thisEffect.Owner)
                    {
                        //Increase the origin card's attack range bonus +1
                        thisEffect.OriginCard.AdjustAttackRangeBonus(1);
                        UpdateEffectLogs("Effect Reacted: Increase the origin card's attack range +1.");
                    }
                    else
                    {
                        //Reduce the origin card's attack range bonus -1
                        thisEffect.OriginCard.AdjustAttackRangeBonus(-1);
                        UpdateEffectLogs("Effect Reacted: Reduced the origin card's attack range -1.");
                    }
                }
                else
                {
                    UpdateEffectLogs("Effect did not react.");
                }
            }
        }
        private void TwinHeadedThunderDragon_RemoveEffect(Effect thisEffect)
        {
            //Remove this effect by reducing the Bonis Attack Range provided to the origin card by this effect
            thisEffect.OriginCard.AdjustAttackRangeBonus(-thisEffect.CustomInt1);

            //Now remove the effect from the active list
            _ActiveEffects.Remove(thisEffect);

            //Update logs
            UpdateEffectLogs("This effect was removed from the active effect list. No revert actions are needed.");
        }
        #endregion

        #region Crest Boosters
        private void CrestBooster_OnSummonActivation(Effect thisEffect, Crest crestToAdd, int amount)
        {
            //Since this is a ON SUMMON EFFECT, display the Effect Panel for 4 secs then execute the effect
            DisplayOnSummonEffectPanel(thisEffect);

            //EFFECT DESCRIPTION: Add whatever [ ] crest to the controller's crest pool by the amout set
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            AdjustPlayerCrestCount(TURNPLAYER, crestToAdd, amount);

            HideEffectMenuPanel();

            //Enter Summon phase 3
            SummonMonster_Phase3(thisEffect.OriginCard);
        }
        #endregion

        #region Trap Hole
        private void TrapHole_TriggerActivation(Effect thisEffect)
        {
            //Step 1: Since this is a TRIGGER, display the Effect Panel to show the effect and cost
            DisplayTriggerEffectPanel(thisEffect);

            //Step 2: Set the "Reaction To" flags
            //This is a OneTime trigger effect. Does not react to events

            //Step 3: Hide the Effect Menu panel
            HideEffectMenuPanel();

            //Step 3: Resolve the effect
            Card summonedCard = CardsBeingSummoned[0];                 
            SpellboundCard(summonedCard, 3);         

            //Step 4: Destroy the card
            DestroyCard(thisEffect.OriginCard.CurrentTile);
            
            //Step 4: Enter Main Phase now
            EnterMainPhase();
        }
        #endregion

        #region Acid Trap Hole
        private void AcidTrapHole_TriggerActivation(Effect thisEffect)
        {
            //Step 1: Since this is a TRIGGER, display the Effect Panel to show the effect and cost
            DisplayTriggerEffectPanel(thisEffect);

            //Step 2: Set the "Reaction To" flags
            //This is a OneTime trigger effect. Does not react to events

            //Step 3: Hide the Effect Menu panel
            HideEffectMenuPanel();

            //Step 3: Resolve the effect
            Card summonedCard = CardsBeingSummoned[0];
            SpellboundCard(summonedCard, 3);

            //Step 4: Destroy the card
            DestroyCard(thisEffect.OriginCard.CurrentTile);

            //Step 4: Enter Main Phase now
            EnterMainPhase();
        }
        #endregion

        #region Banishing Trap Hole
        private void BanishingTrapHole_TriggerActivation(Effect thisEffect)
        {
            //Step 1: Since this is a TRIGGER, display the Effect Panel to show the effect and cost
            DisplayTriggerEffectPanel(thisEffect);

            //Step 2: Set the "Reaction To" flags
            //This is a OneTime trigger effect. Does not react to events

            //Step 3: Hide the Effect Menu panel
            HideEffectMenuPanel();

            //Step 3: Resolve the effect
            Card summonedCard = CardsBeingSummoned[0];
            SpellboundCard(summonedCard, 99);

            //Step 4: Destroy the card
            DestroyCard(thisEffect.OriginCard.CurrentTile);

            //Step 4: Enter Main Phase now
            EnterMainPhase();
        }
        #endregion

        #region Deep Dark Trap Hole
        private void DeepDarkTrapHole_TriggerActivation(Effect thisEffect)
        {
            //Step 1: Since this is a TRIGGER, display the Effect Panel to show the effect and cost
            DisplayTriggerEffectPanel(thisEffect);

            //Step 2: Set the "Reaction To" flags
            //This is a OneTime trigger effect. Does not react to events

            //Step 3: Hide the Effect Menu panel
            HideEffectMenuPanel();

            //Step 3: Resolve the effect
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            Card summonedCard = CardsBeingSummoned[0];
            summonedCard.AdjustAttackBonus(-1000);          

            //Step 4: Destroy the card
            DestroyCard(thisEffect.OriginCard.CurrentTile);

            //Step 4: Enter Main Phase now
            EnterMainPhase();
        }
        #endregion

        #region Treacherous Trap Hole
        private void TreacherousTrapHole_TriggerActivation(Effect thisEffect)
        {
            //Step 1: Since this is a TRIGGER, display the Effect Panel to show the effect and cost
            DisplayTriggerEffectPanel(thisEffect);

            //Step 2: Set the "Reaction To" flags
            //This is a OneTime trigger effect. Does not react to events

            //Step 3: Hide the Effect Menu panel
            HideEffectMenuPanel();

            //Step 3: Resolve the effect
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            Card summonedCard = CardsBeingSummoned[0];
            summonedCard.AdjustDefenseBonus(-1000);          

            //Step 4: Destroy the card
            DestroyCard(thisEffect.OriginCard.CurrentTile);

            //Step 4: Enter Main Phase now
            EnterMainPhase();
        }
        #endregion

        #region Bottomless Trap Hole
        private void BottomlessTrapHole_TriggerActivation(Effect thisEffect)
        {
            //Step 1: Since this is a TRIGGER, display the Effect Panel to show the effect and cost
            DisplayTriggerEffectPanel(thisEffect);

            //Step 2: Set the "Reaction To" flags
            //This is a OneTime trigger effect. Does not react to events

            //Step 3: Hide the Effect Menu panel
            HideEffectMenuPanel();

            //Step 3: Resolve the effect
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            Card summonedCard = CardsBeingSummoned[0];
            SpellboundCard(summonedCard, 99);            

            //Step 4: Destroy the card
            DestroyCard(thisEffect.OriginCard.CurrentTile);

            //Step 4: Enter Main Phase now
            EnterMainPhase();
        }
        #endregion

        #region Adhesion Trap Hole
        private void AdhesionTrapHole_TriggerActivation(Effect thisEffect) 
        {
            //Step 1: Since this is a TRIGGER, display the Effect Panel to show the effect and cost
            DisplayTriggerEffectPanel(thisEffect);

            //Step 2: Set the "Reaction To" flags
            //This is a OneTime trigger effect. Does not react to events

            //Step 3: Hide the Effect Menu panel
            HideEffectMenuPanel();

            //Step 3: Resolve the effect
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            Card summonedCard = CardsBeingSummoned[0];
            summonedCard.AddCannotMoveCounter();           
            UpdateEffectLogs(string.Format("Card [{0}] with OnBoardID [{1}] controlled by [{2}] received a Cannot Move Counter. Cannot Move Counters [{3}]", summonedCard.Name, summonedCard.OnBoardID, summonedCard.Controller, summonedCard.CannotMoveCounters));

            //Step 4: Destroy the card
            DestroyCard(thisEffect.OriginCard.CurrentTile);

            //Step 4: Enter Main Phase now
            EnterMainPhase();
        }
        #endregion

        #region Exodia the Forbidden One
        private void ExodiaTheForbiddenOne_OnSummonActivation(Effect thisEffect)
        {
            //Set the "Reaction To" flags
            //This effect is a One-Time activation, does not reach to any event.

            //EFFECT DESCRIPTION
            //if all "forbidden one" cards are on the board under you control, you win the duel
            bool rightArmfound = false;
            bool leftArmfound = false;
            bool rightLegfound = false;
            bool leftLegfound = false;
            foreach (Card thisCard in _CardsOnBoard)
            {
                if (!thisCard.IsDiscardted && thisCard.Controller == thisEffect.Owner)
                {
                    if (thisCard.Name == "Right Arm of the Forbidden One") { rightArmfound = true; }
                    if (thisCard.Name == "Left Arm of the Forbidden One") { leftArmfound = true; }
                    if (thisCard.Name == "Rigth Leg of the Forbidden One") { rightLegfound = true; }
                    if (thisCard.Name == "Left Leg of the Forbidden One") { leftLegfound = true; }
                }
            }
            UpdateEffectLogs(string.Format("Cards found: Right Arm: [{0}] | Left Arm: [{1}] | Right Leg: [{2}] | Left Leg: [{3}]", rightArmfound, leftArmfound, rightLegfound, leftLegfound));

            //Validate all pieces were found
            if (rightArmfound && leftArmfound && rightLegfound && leftLegfound) 
            {
                DisplayOnSummonEffectPanel(thisEffect);
                HideEffectMenuPanel();

                PicExodiaEnd.Visible = true;
                StartGameOver();
            }
            else
            {
                DisplayOnSummonEffectPanel(thisEffect, "Require cards missing. Effect wont activate.");
                HideEffectMenuPanel();
                SummonMonster_Phase3(thisEffect.OriginCard);
            }
        }
        #endregion

        #region Black Luster Soldier
        private void BlackLusterSoldier_ContinuousActivation(Effect thisEffect)
        {
            //Step 1: Since this is a CONTINUOUS, display the Effect Panel then execute the effect
            DisplayOnSummonContinuousEffectPanel(thisEffect);

            //Step 2: Set the "Reaction To" flags
            thisEffect.ReactsToBattleCalculation = true;

            //Step 3: Resolve the effect
            //This effect doesnt do anything on activation

            //Step 4: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);

            //Step 5: Hide the Effect Menu panel and Enter Summon phase 4
            HideEffectMenuPanel();
            SummonMonster_Phase4(thisEffect.OriginCard);
        }
        private void BlackLusterSoldier_ContinuousREActivation(Effect thisEffect)
        {
            //Step 2: Set the "Reaction To" flags
            thisEffect.ReactsToBattleCalculation = true;

            //Step 3: Resolve the effect
            //This effect doesnt do anything on activation

            //Step 4: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);
        }
        private void BlackLusterSoldier_RemoveEffect(Effect thisEffect)
        {
            //Since this effect doesnt affect any cards while active, there is nothing to "revert"

            //Now remove the effect from the active list
            _ActiveEffects.Remove(thisEffect);

            //Update logs
            UpdateEffectLogs("This effect was removed from the active effect list.");
        }
        private int BlackLusterSoldier_ReactToBattleDamageCalculation(Effect thisEffect, Card Attacker, int OGDamage)
        {
            //This effect will double the battle damage if the Attacker is the origin card
            if(Attacker == thisEffect.OriginCard)
            {
                UpdateEffectLogs(string.Format("Effect Reacted: Effect [{0}] doubled the battle damage calculated since the Origin Card is the Attacker.", thisEffect.ID));
                DisplayReactionEffectNotification(thisEffect, thisEffect.EffectText);
                HideReactionNotification();
                return OGDamage * 2;
            }
            else
            {
                return OGDamage;
            }
        }
        #endregion

        #region Shinato, King of a Higher Plane
        private void ShinatoKingOfAHigherPlane_ContinuousActivation(Effect thisEffect)
        {
            //Step 1: Since this is a CONTINUOUS, display the Effect Panel then execute the effect
            DisplayOnSummonContinuousEffectPanel(thisEffect);

            //Step 2: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterDestroyedByBattle = true;

            //Step 3: Resolve the effect
            //This effect doesnt do anything on activation

            //Step 4: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);

            //Step 5: Hide the Effect Menu panel and Enter Summon phase 4
            HideEffectMenuPanel();
            SummonMonster_Phase4(thisEffect.OriginCard);
        }
        private void ShinatoKingOfAHigherPlane_ContinuousREActivation(Effect thisEffect)
        {
            //Step 2: Set the "Reaction To" flags
            thisEffect.ReactsToMonsterDestroyedByBattle = true;

            //Step 3: Resolve the effect
            //This effect doesnt do anything on activation

            //Step 4: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);
        }
        private void ShinatoKingOfAHigherPlane_RemoveEffect(Effect thisEffect)
        {
            //Since this effect doesnt affect any cards while active, there is nothing to "revert"

            //Now remove the effect from the active list
            _ActiveEffects.Remove(thisEffect);

            //Update logs
            UpdateEffectLogs("This effect was removed from the active effect list.");
        }
        private void ShinatoKingOfAHigherPlane_ReactToMonsterDestryedByBattle(Effect thisEffect, Card Attacker, Card Defender)
        {
            //This effect will deal damage to the opponent's symbol, if the origin card was the attacker and 
            //the defender is now destroyed
            if (Attacker == thisEffect.OriginCard && Defender.IsDiscardted)
            {
                UpdateEffectLogs(string.Format("Effect Reacted: Effect ID [{0}] | Inflict damage to the opponent's symbol equal to the defender original ATK [{1}]", thisEffect.ID, Defender.OriginalATK));
                //The damage to be deal is equal to the destroyed defender monster's original ATK
                int damage = Defender.OriginalATK;

                //Open the effect reaction notification
                DisplayReactionEffectNotification(thisEffect, thisEffect.EffectText);

                //Stablish the Defender Symbol
                Card DefenderSymbol = _RedSymbol;
                Label DefenderSymbolLPLabel = lblRedLP;
                if (TURNPLAYER == PlayerColor.RED) { DefenderSymbol = _BlueSymbol; DefenderSymbolLPLabel = lblBlueLP; }
                DealDamageToSymbol(damage, DefenderSymbol, DefenderSymbolLPLabel);

                //Now hide the reaction notification
                HideReactionNotification();
            }
        }
        #endregion

        #region Petit Moth
        private void PetitMoth_IgnitionActivation(Effect thisEffect)
        {
            //Hide the Effect Menu 
            HideEffectMenuPanel();

            //And Resolve the effect
            //EFFECT DESCRIPTION: Transform this monster into “Cocoon of Evolution” (ID 40240595)
            TransformMonster(_EffectOriginTile, 40240595);

            //NO more action needed, return to the Main Phase
            EnterMainPhase();
        }
        #endregion

        #region Cocoon of Evolution
        private void CocoonofEvolution_ContinuousActivation(Effect thisEffect)
        {
            //Step 1: Since this is a CONTINUOUS, display the Effect Panel for 4 secs then execute the effect
            DisplayOnSummonContinuousEffectPanel(thisEffect);

            //Step 2: Set the "Reaction To" flags
            thisEffect.ReactsToEndPhase = true;

            //Step 3: Resolve the effect
            //EFFECT DESCRIPTION: During each of your opponent’s End Phases, if this monster was transformed into; place 1 Turn Counter on it.
            //This effect does not do anything during activation


            //Step 4: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);

            //Step 5: Hide the Effect Menu panel
            HideEffectMenuPanel();
            //Enter Summon phase 4
            SummonMonster_Phase4(thisEffect.OriginCard);
        }
        private void CocoonofEvolution_ContinuousREActivation(Effect thisEffect)
        {
            //Step 1: Set the "Reaction To" flags
            thisEffect.ReactsToEndPhase = true;

            //Step 2: Resolve the effect
            //EFFECT DESCRIPTION: During each of your opponent’s End Phases, if this monster was transformed into; place 1 Turn Counter on it.
            //This effect does not do anything during activation

            //Step 3: Add this effect to the Active Effect list
            _ActiveEffects.Add(thisEffect);
        }
        private void CocoonofEvolution_RemoveEffect(Effect thisEffect)
        {
            //Remove this effect without taking any action

            //Now remove the effect from the active list
            _ActiveEffects.Remove(thisEffect);

            //Update logs
            UpdateEffectLogs("This effect was removed from the active effect list without taking any action.");
        }
        private void CocoonofEvolution_ReactTo_EndPhase(Effect thisEffect)
        {
            //Reaction description: 
            //During each of your opponent’s End Phases, if this monster was transformed into; place 1 Turn Counter on it.
            if(thisEffect.OriginCard.WasTransformedInto && TURNPLAYER != thisEffect.Owner)
            {
                //Open the effect reaction notification
                DisplayReactionEffectNotification(thisEffect, thisEffect.EffectText);
                thisEffect.OriginCard.PlaceTurnCounter();
                UpdateEffectLogs("Effect reacted, Turn Counter placed on origin card.");

                //Now hide the reaction notification
                HideReactionNotification();
            }
            else
            {
                UpdateEffectLogs("Effect's Origin Card was not transformed into and/or is not the opponent's end phase, effect did not react.");
            }
        }
        private void CocoonofEvolution_IgnitionActivation(Effect thisEffect)
        {
            //Hide the Effect Menu 
            HideEffectMenuPanel();

            //And Resolve the effect
            //EFFECT DESCRIPTION: Transform this monster based on the amount of turn counters it has:
            //2-4: Larvae Moth (ID 87756343)
            //5-6: Great Moth (ID 14141448)
            //7+: Perfectly Ultimate Great Moth (ID 48579379)
            int id = -1;
            if (thisEffect.OriginCard.TurnCounters <= 4) { id = 87756343; }
            else if (thisEffect.OriginCard.TurnCounters <= 6) { id = 14141448; }
            else { id = 48579379; }

            TransformMonster(_EffectOriginTile, id);
        }
        #endregion

        #region Larvae Moth
        private void LarvaeMoth_OnSummonActivation(Effect thisEffect)
        {
            //Since this is a ON SUMMON EFFECT, display the Effect Panel for 4 secs then execute the effect

            if (!thisEffect.OriginCard.WasTransformedInto)
            {
                //Effect wont be activated, display with Effect Menu panel with the missing requirements
                DisplayOnSummonEffectPanel(thisEffect, "Cannot activate if monster was not transformed into. Effect wont activate.");
                UpdateEffectLogs("Monster was not transformed into, effect wont activate");
                HideEffectMenuPanel();
                //Enter Summon phase 3
                SummonMonster_Phase3(thisEffect.OriginCard);
            }
            else
            {
                DisplayOnSummonEffectPanel(thisEffect);

                //EFFECT DESCRIPTION: If this monster was transformed into; Add 5 [DEF] to your crest pool.
                AdjustPlayerCrestCount(TURNPLAYER, Crest.DEF, 5);

                HideEffectMenuPanel();

                //Enter Summon phase 3
                SummonMonster_Phase3(thisEffect.OriginCard);
            }           
        }
        #endregion

        #region Greath Moth
        private void GreatMoth_OnSummonActivation(Effect thisEffect)
        {
            //ON SUMMON EFFECT will not activate if the monster was NOT Transform Summoned (Transformed into)
            //And there is no other insect type monster on the board the effect's owner controls
            bool activationRequirementsMet = false;
            _EffectTargetCandidates.Clear();
            if (thisEffect.OriginCard.WasTransformedInto)
            {
                foreach (Card thisCard in _CardsOnBoard)
                {
                    if (!thisCard.IsDiscardted && thisCard != thisEffect.OriginCard && thisCard.Type == Type.Insect && thisCard.Controller == thisEffect.Owner)
                    {
                        activationRequirementsMet = true;
                        _EffectTargetCandidates.Add(thisCard.CurrentTile);
                    }
                }
                UpdateEffectLogs(string.Format("Target candidates found: [{0}], player will be prompt to target a monster in the UI.", _EffectTargetCandidates.Count));
            }

            if (activationRequirementsMet)
            {
                //Proceed with the effect activation path
                DisplayOnSummonEffectPanel(thisEffect);
                HideEffectMenuPanel();

                //Set the "Reaction To" flags
                //This effect is a One-Time activation, does not reach to any event.

                //The Target selection will handle takin the player to the next game state
                _CurrentPostTargetState = PostTargetState.GreatMothEffect;
                InitializeEffectTargetSelection();
            }
            else
            {
                //Effect wont be activated, display with Effect Menu panel with the missing requirements
                if (!thisEffect.OriginCard.WasTransformedInto) 
                {
                    DisplayOnSummonEffectPanel(thisEffect, "Cannot activate when monster was NOT transformed into. Effect wont activate.");
                    UpdateEffectLogs("Monster was transformed into, effect wont activate");
                }
                else
                {
                    DisplayOnSummonEffectPanel(thisEffect, "No valid targets available. Effect wont activate.");
                    UpdateEffectLogs("There are no valid effect targets, effect wont activate");
                }               
                HideEffectMenuPanel();
                //Enter Summon phase 3
                SummonMonster_Phase3(thisEffect.OriginCard);
            }
        }
        private void GreatMoth_PostTargetEffect(Tile TargetTile)
        {
            //Now apply the effect into the target

            //Resolve the effect: Target monster gains 700 ATK/DEF
            TargetTile.CardInPlace.AdjustAttackBonus(700);
            TargetTile.CardInPlace.AdjustDefenseBonus(700);

            //Enter Summon phase 3
            SummonMonster_Phase3(CardsBeingSummoned[0]);
        }
        #endregion
    }
}