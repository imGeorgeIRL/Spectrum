INCLUDE globals.ink 

{Day_Of_Week:
- 0: ->Monday
- 1: -> Tuesday
}

===Monday===
{isLateToClass : 

I dont think ill catch the wrong bus tomorrow#speaker:Austin
That was embarrasing
I'm sure class tomorrow will be fine
~bedTime(1)
- else:
Today went well!#speaker:Austin
I'm glad nothing went wrong,
that would've been awful.
~bedTime(1)
~Day_Of_Week = 1
}
anyway, sleep time!
~Day_Of_Week = 1
~calendar_Interactions = 0
~bedside_Interactions = 0
~isBedTime = false
~fridge_Interactions = 0
~hasEatenDinner = false
~isNight = false
~bus_Chosen = false
~isLateToClass = false
~FoundBus = false
~noah_Interractions = 0
~spokenToNoah = false
~ spokenToMiller = false
~ houseNPC1 = false
~ houseNPC2 = false
~ houseNPC3 = false
~ townNPC1 = false
~ townNPC2 = false
~ townNPC3 = false
->DONE


===Tuesday===
That was #speaker: Austin
    *[awful]
    *[terrible]
    *[horrible]
-It's not even my bed time yet 
but I really just need to sleep.
I really just dont know
why everything is going wrong.
My meltdown today probably scared Noah off
My one chance at making a friend
and I ruined it by just being me. 
~Day_Of_Week = 2
~bedTime(1)
~calendar_Interactions = 0
~isBedTime = false
~fridge_Interactions = 0
~hasEatenDinner = false
~isNight = false
~bus_Chosen = false
~FoundBus = false
~ houseNPC1 = false
~ houseNPC2 = false
~ houseNPC3 = false
~ townNPC1 = false
~ townNPC2 = false
~ townNPC3 = false
~noah_Interractions = 0
->DONE


