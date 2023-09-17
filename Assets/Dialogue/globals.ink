// GENERAL
VAR Day_Of_Week = 1 //should be 0

VAR cafeOnMonday = false 
VAR busOnMonday = false

VAR sceneInDay = ""

// BEDROOM

VAR calendar_Interactions = 0
VAR bedside_Interactions = 0
VAR isBedTime = false
EXTERNAL bedTime(value)
~bedTime(0)

EXTERNAL calendarInteract(cal)

//KITCHEN LIVING

VAR fridge_Interactions = 0
VAR hasEatenDinner = false
VAR isNight = false

EXTERNAL talkedToMum(mum)

EXTERNAL tvChoice(choice)

EXTERNAL watchTv(watched)
~watchTv(0)

//BUS 
VAR bus_Chosen = false
VAR isLateToClass = false
VAR FoundBus = false
EXTERNAL busChosen(bus)

VAR tuesdayUni = false 

//NOAH 
VAR noah_Interractions = 0
VAR spokenToNoah = false 
VAR noahInCafe = false 
EXTERNAL timeSkip(skip)
EXTERNAL calmDown(calm)
EXTERNAL noahWalkAway(walk)

VAR deepConversation = false
EXTERNAL deepConvoBreak(breaks)
VAR deepConvoPause = false
EXTERNAL tuesdayMeltdown(melt)

//CAFE 
VAR drink = ""
VAR size = ""
VAR cost = 0

VAR orderedDrink = false

//Threads
VAR clothesChosen = 0
EXTERNAL waitTime(time)
VAR costOfClothes = 0

//MISC
EXTERNAL makeChoice(choice)


EXTERNAL spokeTo(person)


EXTERNAL turnNight(night)


EXTERNAL sittingDown(sit)

EXTERNAL drMiller(interact)



EXTERNAL decreaseSens(sens)
EXTERNAL increaseSens(sens)

//UNI 
VAR spokenToMiller = false


//NPCS
VAR houseNPC1 = false
VAR donnyInteractions = 0

VAR houseNPC2 = false
VAR charlesInteractions = 0

VAR houseNPC3 = false
VAR sanjayInteractions = 0

VAR townNPC1 = false 
VAR town1 = 1 //should be 0

VAR townNPC2 = false
VAR town2 = 0

VAR townNPC3 = false
VAR town3 = 0

//Noah
EXTERNAL spawnNoah(spawn)
VAR NoahWednesdayTown = false

//observatory
VAR giftsBought = 0
VAR boughtForNoah = false
VAR brainyGizato = false

EXTERNAL watch(sit)
EXTERNAL meteor(shower)
EXTERNAL panicText(panic)