// GENERAL
VAR Day_Of_Week = "0"


// BEDROOM

VAR calendar_Interactions = 0
VAR bedside_Interactions = 0
VAR isBedTime = false
EXTERNAL bedTime(value)

//KITCHEN

VAR fridge_Interactions = 0

//BUS 
VAR bus_Chosen = false
VAR isLateToClass = false
VAR FoundBus = false

//NOAH 
VAR noah_Interractions = 0
VAR spokenToNoah = false



EXTERNAL makeChoice(choice)

EXTERNAL spokeTo(person)

EXTERNAL turnNight(night)

EXTERNAL dailyTasks(task)

EXTERNAL sittingDown(sit)



