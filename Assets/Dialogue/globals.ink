// GENERAL
VAR Day_Of_Week = "0"


// BEDROOM

VAR calendar_Interactions = 0
VAR bedside_Interactions = 0
VAR isBedTime = false
EXTERNAL bedTime(value)
~bedTime(0)

//KITCHEN LIVING

VAR fridge_Interactions = 0
VAR hasEatenDinner = false
VAR isNight = false

EXTERNAL watchTv(watched)
~watchTv(0)

//BUS 
VAR bus_Chosen = false
VAR isLateToClass = false
VAR FoundBus = false
EXTERNAL busChosen(bus)

//NOAH 
VAR noah_Interractions = 0
VAR spokenToNoah = false
EXTERNAL timeSkip(skip)
~timeSkip(0)

EXTERNAL makeChoice(choice)
~makeChoice(0)

EXTERNAL spokeTo(person)
~spokeTo(0)

EXTERNAL turnNight(night)
~turnNight(0)

EXTERNAL sittingDown(sit)
~sittingDown(0)



