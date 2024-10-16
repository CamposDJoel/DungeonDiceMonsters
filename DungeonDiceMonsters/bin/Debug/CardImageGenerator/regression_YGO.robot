*** Setting ***
Library				Selenium2Library
Library				String
Library				Collections
Library				OperatingSystem
Library 			DateTime

Suite Setup 		YGO_Setup
# Suite Teardown		SuiteTeardown

Test Setup			YGO_Login to Card Maker

*** Variables ***
${WIN_POS}				-2000
${DBFile}         ${EXECDIR}/DB.txt
${ArtworkFileName}
@{URLList}

*** Test Case ***
TC-YGO-001 Create Card Images
	####
	${FILE_CONTENT}=		Get File			${DBFile}
	@{LINES}=				Split To Lines		${FILE_CONTENT}
	
	: FOR		${LINE}			IN			@{LINES}
	\			@{COLUMNS}=					Split String		${LINE}			separator=|
	\			YGO_Fillout Card Data		${COLUMNS}
	#####

TC-YGO-002 Create Artwork URL list file
	####
	${FILE_CONTENT}=		Get File			${DBFile}
	@{LINES}=				Split To Lines		${FILE_CONTENT}
	
	@{URLList}=				Create List
	
	: FOR		${LINE}			IN			@{LINES}
	\			@{COLUMNS}=					Split String		${LINE}			separator=|
	\			${cardid}=					Get From List       ${COLUMNS}		1
	\			Append to List				${URLList}			https://images.ygoprodeck.com/images/cards_cropped/${cardid}.jpg
	
	#//Create the final File
	${FileData}=			Create File String		${URLList}
	${FileData}=			Set Variable			${FileData}
	Create File				${EXECDIR}/ArtworkURLList.txt		${FileData}
	
	
	#####

*** Keywords 
Wait and Click
	####
	[Arguments]		${xpath}
	Wait Until Element Is Visible		${xpath}
	Element_Scroll To Element			${xpath}
	Click Element		${xpath}
	#####
	
YGO_Setup
	####
	#//Open the Browser
	Set Selenium Timeout				15
	${chrome_options}=    				Evaluate    sys.modules['selenium.webdriver.chrome.options'].Options()    sys, selenium.webdriver.chrome.options
    Create Webdriver    				Chrome
	#//Go To Konami DB Card Search Page
	Sleep								1s
	Set Window Position					${WIN_POS}		0
	Maximize Browser Window
	#####
	
YGO_Login to Card Maker
	####
	Go To			https://www.cardmaker.net/yugioh
	Wait and Click	//a[@id="elUserSignIn"]
	input text		//input[@name="auth"]			joelcloud
	input text		//input[@name="password"]		Eltlacua5!
	Wait and Click	//button[@value="usernamepassword"]
	
	Wait Until Element Is Visible	//div[@data-role="profileWidget"]
	#####
	
YGO_Fillout Card Data
	####
	[Arguments]		${COLUMNS}
	${cardname}=			Get From List       ${COLUMNS}		0
	${cardid}=				Get From List       ${COLUMNS}		2
	${cardcategory}=		Get From List       ${COLUMNS}		3
	Run Keyword if		"${cardcategory}"=="Monster"		YGO_Fillout Monster Card		${COLUMNS}
	Run Keyword if		"${cardcategory}"=="Spell"			YGO_Fillout Spell Card			${COLUMNS}
	Run Keyword if		"${cardcategory}"=="Trap"			YGO_Fillout Trap Card			${COLUMNS}
	
	#//Save the card and download
	Wait And Click						//button[.='Save']
	Sleep			3s
	Wait and Click						//div[@class="items-center flex flex-col flex-1"]/div[2]/div[1]//button
	Wait and Click						//div[@id="menu-item-0"][2]
	Sleep			3s
	Element_Scroll To Element			//input[@id="name"]	
	
	#//Click "new card" to set up the page for the next card
	Click Element						//div[@class="my-cards flex-1 css-2b097c-container"]/div/div[2]
	Wait And Click						//button[.='New Card']
	Sleep			2s
	
	#//Rename file
	Move File			C:\\Users\\DizziDesktop\\Downloads\\${cardname}.jpeg			C:\\Users\\DizziDesktop\\Downloads\\finalcards\\${cardid}.jpeg
	# Move File			C:\\Users\\campo\\Downloads\\${cardname}.jpeg			C:\\Users\\campo\\Downloads\\finalcards\\${cardid}.jpeg
	#####

YGO_Fillout Monster Card
	####
	[Arguments]		${COLUMNS}
	${cardname}=			Get From List       ${COLUMNS}		0
	${cardnumber}=			Get From List       ${COLUMNS}		1
	${cardid}=				Get From List       ${COLUMNS}		2
	${cardcategory}=		Get From List       ${COLUMNS}		3
	${cardtype}=			Get From List       ${COLUMNS}		4
	${cardsectype}=			Get From List       ${COLUMNS}		5
	${cardattribute}=		Get From List       ${COLUMNS}		6
	${cardatk}=				Get From List       ${COLUMNS}		7
	${carddef}=				Get From List       ${COLUMNS}		8
	${cardlp}=				Get From List       ${COLUMNS}		9
	${cardmonsterlevel}=	Get From List       ${COLUMNS}		10
	${carddicelevel}=		Get From List       ${COLUMNS}		11
	${cardface1}=			Get From List       ${COLUMNS}		12
	${cardface2}=			Get From List       ${COLUMNS}		13
	${cardface3}=			Get From List       ${COLUMNS}		14
	${cardface4}=			Get From List       ${COLUMNS}		15
	${cardface5}=			Get From List       ${COLUMNS}		16
	${cardface6}=			Get From List       ${COLUMNS}		17
	${OnSummonEffect}=		Get From List       ${COLUMNS}		18
	${ContinuEffect}=		Get From List       ${COLUMNS}		19
	${Ability}=				Get From List       ${COLUMNS}		20
	${IgnitionEffect}=		Get From List       ${COLUMNS}		21
	${TriggerEffect}=		Get From List       ${COLUMNS}		22
	${EquipEffect}=			Get From List       ${COLUMNS}		23
	${FusionMaterial1}=		Get From List       ${COLUMNS}		24
	${FusionMaterial2}=		Get From List       ${COLUMNS}		25
	${FusionMaterial3}=		Get From List       ${COLUMNS}		26
	${RitualSpell}=			Get From List       ${COLUMNS}		27
	
	${cardtext}=			Set Variable		${EMPTY}
	
	${FusionMaterials}=		Set Variable		"${FusionMaterial1}"+"${FusionMaterial2}"
	${FusionMaterials}=		Run Keyword If		"${FusionMaterial3}"!="-"		Set Variable		${FusionMaterials}+"${FusionMaterial3}"		ELSE	Set Variable		${FusionMaterials}
	
	#//Create the card text
	#//Step 1: if the monster is a fusion set the fusion materials first
	${cardtext}=			Run Keyword if		"${cardsectype}"=="Fusion"		Set Variable		${FusionMaterials}\n\n		ELSE		Set Variable		${cardtext}
	#//Step 2: Add the effects
	${cardtext}=			Run Keyword if		"${Ability}"!="-"				Set Variable		${cardtext} (ABILITY) - ${Ability}\n\n				ELSE		Set Variable		${cardtext}
	${cardtext}=			Run Keyword if		"${OnSummonEffect}"!="-"		Set Variable		${cardtext} (ON SUMMON) - ${OnSummonEffect}\n\n		ELSE		Set Variable		${cardtext}
	${cardtext}=			Run Keyword if		"${ContinuEffect}"!="-"			Set Variable		${cardtext} (CONTINUOUS) - ${ContinuEffect}\n\n		ELSE		Set Variable		${cardtext}
	${cardtext}=			Run Keyword if		"${IgnitionEffect}"!="-"		Set Variable		${cardtext} (EFFECT) - ${IgnitionEffect}\n\n		ELSE		Set Variable		${cardtext}
	${cardtext}=			Run Keyword if		"${TriggerEffect}"!="-"			Set Variable		${cardtext} (TRIGGER) - ${TriggerEffect}\n\n		ELSE		Set Variable		${cardtext}
	${cardtext}=			Set Variable		${cardtext}Dice Lv.${carddicelevel} [${cardface1}][${cardface2}][${cardface3}][${cardface4}][${cardface5}][${cardface6}]
	
	#//Set the filenae directory for the artwork to be used
	Set Suite Variable	${ArtworkFileName}		${EXECDIR}/Artwork/${cardid}.jpg
	#//mod the attribute value from ALL CAPS to only first letter cap
	${cardattribute}=		YGO_Mod Attribute	${cardattribute}
	
	#//Card Name
	input text			//input[@id="name"]		${cardname}
	#//Card Color
	Element_Select From Dropdown		//div[@id="root"]/div/div[2]/div/div/div[2]//select				${cardsectype}
	#//Card Attribute
	Element_Select From Dropdown		//div[@id="root"]/div/div[2]/div/div/div[3]/div[1]//select		${cardattribute}
	#//Monster Level
	Element_Select From Dropdown		//div[@id="root"]/div/div[2]/div/div/div[4]//select				${cardmonsterlevel}
	#//Artwork
	Choose File							//div[@id="root"]/div/div[2]/div/div/div[5]/div/div[2]/input	${ArtworkFileName}
	#//Monster Type/Sectype/LP
	input text							//div[@id="root"]/div/div[2]/div/div/div[7]//input				${cardtype}/${cardsectype}/LP:${cardlp}
	#//Card text						
	input text							//div[@id="root"]/div/div[2]/div/div/div[8]//textarea			${cardtext}	
	#//Stats
	input text							//div[@id="root"]/div/div[2]/div/div/div[9]/div[1]//input		${cardatk}
	input text							//div[@id="root"]/div/div[2]/div/div/div[9]/div[2]//input		${carddef}
	#//extra data
	input text							//div[@id="root"]/div/div[2]/div/div/div[10]/div[1]//input		DDM
	input text							//div[@id="root"]/div/div[2]/div/div/div[10]/div[2]//input		${cardnumber}
	input text							//div[@id="root"]/div/div[2]/div/div/div[11]/div[1]//input		${cardid}
	input text							//div[@id="root"]/div/div[2]/div/div/div[11]/div[2]//input		1st Edition
	input text							//div[@id="root"]/div/div[2]/div/div/div[12]/div[1]//input		2024
	input text							//div[@id="root"]/div/div[2]/div/div/div[12]/div[2]//input		CamposD.Joel
	input text							//div[@id="root"]/div/div[2]/div/div/div[13]/div[1]//input		Dungeon Dice Monsters
	#####
	
YGO_Fillout Spell Card
	####
	[Arguments]		${COLUMNS}
	${cardname}=			Get From List       ${COLUMNS}		0
	${cardnumber}=			Get From List       ${COLUMNS}		1
	${cardid}=				Get From List       ${COLUMNS}		2	
	${cardtype}=			Get From List       ${COLUMNS}		4
	${carddicelevel}=		Get From List       ${COLUMNS}		11
	${cardface1}=			Get From List       ${COLUMNS}		12
	${cardface2}=			Get From List       ${COLUMNS}		13
	${cardface3}=			Get From List       ${COLUMNS}		14
	${cardface4}=			Get From List       ${COLUMNS}		15
	${cardface5}=			Get From List       ${COLUMNS}		16
	${cardface6}=			Get From List       ${COLUMNS}		17
	${ContinuEffect}=		Get From List       ${COLUMNS}		19
	${IgnitionEffect}=		Get From List       ${COLUMNS}		21
	${TriggerEffect}=		Get From List       ${COLUMNS}		22
	${EquipEffect}=			Get From List       ${COLUMNS}		23
	
	${cardtext}=			Set Variable		${EMPTY}
	${cardtext}=			Run Keyword if		"${ContinuEffect}"!="-"			Set Variable		(CONTINUOUS) - ${ContinuEffect}\n\n	ELSE		Set Variable		${cardtext}
	${cardtext}=			Run Keyword if		"${IgnitionEffect}"!="-"		Set Variable		(EFFECT) - ${IgnitionEffect}\n\n		ELSE		Set Variable		${cardtext}
	${cardtext}=			Run Keyword if		"${TriggerEffect}"!="-"			Set Variable		(TRIGGER) - ${TriggerEffect}\n\n		ELSE		Set Variable		${cardtext}
	${cardtext}=			Run Keyword if		"${EquipEffect}"!="-"			Set Variable		${cardtext} (EQUIP) - ${EquipEffect}\n\n			ELSE		Set Variable		${cardtext}
	${cardtext}=			Set Variable		${cardtext}Dice Lv.${carddicelevel} [${cardface1}][${cardface2}][${cardface3}][${cardface4}][${cardface5}][${cardface6}]
	
	
	${cardtype}=			Run Keyword if		"${cardtype}"=="Normal"			Set Variable		None		ELSE		Set Variable		${cardtype}
	
	Set Suite Variable	${ArtworkFileName}		${EXECDIR}/Artwork/${cardid}.jpg
	
	#//Card Name
	input text			//input[@id="name"]		${cardname}
	#//Card Color
	Element_Select From Dropdown		//div[@id="root"]/div/div[2]/div/div/div[2]//select				Spell
	#//Spell Type
	Element_Select From Dropdown		//div[@id="root"]/div/div[2]/div/div/div[2]/div[2]//select		${cardtype}
	#//Artwork
	Choose File							//div[@id="root"]/div/div[2]/div/div/div[4]/div/div[2]/input	${ArtworkFileName}
	#//Card text						
	input text							//div[@id="root"]/div/div[2]/div/div/div[6]//textarea			${cardtext}
	#//extra data
	input text							//div[@id="root"]/div/div[2]/div/div/div[7]/div[1]//input		DDM
	input text							//div[@id="root"]/div/div[2]/div/div/div[7]/div[2]//input		${cardnumber}
	input text							//div[@id="root"]/div/div[2]/div/div/div[8]/div[1]//input		${cardid}
	input text							//div[@id="root"]/div/div[2]/div/div/div[8]/div[2]//input		1st Edition
	input text							//div[@id="root"]/div/div[2]/div/div/div[9]/div[1]//input		2024
	input text							//div[@id="root"]/div/div[2]/div/div/div[9]/div[2]//input		CamposD.Joel
	input text							//div[@id="root"]/div/div[2]/div/div/div[10]/div[1]//input		Dungeon Dice Monsters
	#####
	
YGO_Fillout Trap Card
	####
	[Arguments]		${COLUMNS}
	${cardname}=			Get From List       ${COLUMNS}		0
	${cardnumber}=			Get From List       ${COLUMNS}		1
	${cardid}=				Get From List       ${COLUMNS}		2	
	${cardtype}=			Get From List       ${COLUMNS}		4
	${carddicelevel}=		Get From List       ${COLUMNS}		11
	${cardface1}=			Get From List       ${COLUMNS}		12
	${cardface2}=			Get From List       ${COLUMNS}		13
	${cardface3}=			Get From List       ${COLUMNS}		14
	${cardface4}=			Get From List       ${COLUMNS}		15
	${cardface5}=			Get From List       ${COLUMNS}		16
	${cardface6}=			Get From List       ${COLUMNS}		17
	${ContinuEffect}=		Get From List       ${COLUMNS}		19
	${IgnitionEffect}=		Get From List       ${COLUMNS}		21
	${TriggerEffect}=		Get From List       ${COLUMNS}		22
	${EquipEffect}=			Get From List       ${COLUMNS}		23
	
	${cardtext}=			Set Variable		${EMPTY}
	${cardtext}=			Run Keyword if		"${ContinuEffect}"!="-"			Set Variable		(CONTINUOUS) - ${ContinuEffect}\n\n	ELSE		Set Variable		${cardtext}
	${cardtext}=			Run Keyword if		"${IgnitionEffect}"!="-"		Set Variable		(EFFECT) - ${IgnitionEffect}\n\n		ELSE		Set Variable		${cardtext}
	${cardtext}=			Run Keyword if		"${TriggerEffect}"!="-"			Set Variable		(TRIGGER) - ${TriggerEffect}\n\n		ELSE		Set Variable		${cardtext}
	${cardtext}=			Run Keyword if		"${EquipEffect}"!="-"			Set Variable		${cardtext} (EQUIP) - ${EquipEffect}\n\n			ELSE		Set Variable		${cardtext}
	${cardtext}=			Set Variable		${cardtext}Dice Lv.${carddicelevel} [${cardface1}][${cardface2}][${cardface3}][${cardface4}][${cardface5}][${cardface6}]
	
	
	${cardtype}=			Run Keyword if		"${cardtype}"=="Normal"			Set Variable		None
	
	Set Suite Variable	${ArtworkFileName}		${EXECDIR}/Artwork/${cardid}.jpg
	
	#//Card Name
	input text			//input[@id="name"]		${cardname}
	#//Card Color
	Element_Select From Dropdown		//div[@id="root"]/div/div[2]/div/div/div[2]//select				Trap
	#//Spell Type
	Element_Select From Dropdown		//div[@id="root"]/div/div[2]/div/div/div[2]/div[2]//select		${cardtype}
	#//Artwork
	Choose File							//div[@id="root"]/div/div[2]/div/div/div[4]/div/div[2]/input	${ArtworkFileName}
	#//Card text						
	input text							//div[@id="root"]/div/div[2]/div/div/div[6]//textarea			${cardtext}
	#//extra data
	input text							//div[@id="root"]/div/div[2]/div/div/div[7]/div[1]//input		DDM
	input text							//div[@id="root"]/div/div[2]/div/div/div[7]/div[2]//input		${cardnumber}
	input text							//div[@id="root"]/div/div[2]/div/div/div[8]/div[1]//input		${cardid}
	input text							//div[@id="root"]/div/div[2]/div/div/div[8]/div[2]//input		1st Edition
	input text							//div[@id="root"]/div/div[2]/div/div/div[9]/div[1]//input		2024
	input text							//div[@id="root"]/div/div[2]/div/div/div[9]/div[2]//input		CamposD.Joel
	input text							//div[@id="root"]/div/div[2]/div/div/div[10]/div[1]//input		Dungeon Dice Monsters
	#####
	
YGO_Mod Attribute
	####
	[Arguments]		${Original}
	${new}=			Set Variable		${Original}
	${new}=			Run Keyword if		"${Original}"=="DARK"		Set Variable	Dark		ELSE	Set Variable	${new}
	${new}=			Run Keyword if		"${Original}"=="LIGHT"		Set Variable	Light		ELSE	Set Variable	${new}
	${new}=			Run Keyword if		"${Original}"=="WATER"		Set Variable	Water		ELSE	Set Variable	${new}
	${new}=			Run Keyword if		"${Original}"=="FIRE"		Set Variable	Fire		ELSE	Set Variable	${new}
	${new}=			Run Keyword if		"${Original}"=="EARTH"		Set Variable	Earth		ELSE	Set Variable	${new}
	${new}=			Run Keyword if		"${Original}"=="WIND"		Set Variable	Wind		ELSE	Set Variable	${new}
	${new}=			Run Keyword if		"${Original}"=="DIVINE"		Set Variable	Divine		ELSE	Set Variable	${new}
	[Return]		${new}
	#####
	
Element_Select From Dropdown
	####
	[Arguments]		${baseDD}	${option}
	Wait and Click	${baseDD}
	Wait and Click	${baseDD}//option[.='${option}']
	#####
	
Element_Scroll To Element
	####
	[Arguments]		${xpath}
	${y}=				Get Vertical Position		${xpath}
	${scroll}=			Evaluate		int(float("${y}")) - 100
	Execute Javascript							scrollTo(0, ${scroll})
	#####
	
Create File String
	####
	[Arguments]		${list}
	${FileString}=	Set Variable	${EMPTY}
	:FOR	${item}	IN		@{list}
	\		${FileString}=		Set Variable	${FileString}${item}\n
	[Return]		${FileString}
	#####