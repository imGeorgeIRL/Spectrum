// GENERAL
VAR Day_Of_Week = "0"


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


EXTERNAL makeChoice(choice)


EXTERNAL spokeTo(person)


EXTERNAL turnNight(night)


EXTERNAL sittingDown(sit)

EXTERNAL drMiller(interact)


