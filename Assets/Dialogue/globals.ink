// GENERAL
VAR Day_Of_Week = 1 //changed for testing purposes

VAR cafeOnMonday = true //changed for testing
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

VAR tuesdayUni = true //var changed for testing 

//NOAH 
VAR noah_Interractions = 0
VAR spokenToNoah = true //var changed for testing 
VAR noahInCafe = true //var changed testing
EXTERNAL timeSkip(skip)
EXTERNAL calmDown(calm)
EXTERNAL noahWalkAway(walk)

VAR deepConversation = false
EXTERNAL deepConvoBreak(breaks)
VAR deepConvoPause = false

//CAFE 
VAR drink = ""
VAR size = ""
VAR cost = 0

VAR orderedDrink = false


//MISC
EXTERNAL makeChoice(choice)


EXTERNAL spokeTo(person)


EXTERNAL turnNight(night)


EXTERNAL sittingDown(sit)

EXTERNAL drMiller(interact)

//UNI 
VAR spokenToMiller = false


//NPCS
VAR houseNPC1 = false
VAR houseNPC2 = false
VAR houseNPC3 = false

VAR townNPC1 = false
VAR townNPC2 = false
VAR townNPC3 = false