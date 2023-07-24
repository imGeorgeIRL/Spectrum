INCLUDE globals.ink 

{Day_Of_Week:
- 0: ->Monday
- 1: -> Tuesday
- 2: -> Wednesday
- 3: -> Thursday
- 4: -> Friday
}

===Monday===
{townNPC3 : ->Monday2 }
what? #speaker:Guy
uh... #speaker:Austin
spit it out dude. #speaker:Guy
sorry #speaker:Austin
~spokeTo(1)
~townNPC3 = true
bye
->DONE

===Monday2===
What do you want? #speaker:Guy
nothing, sorry #speaker:Austin
->DONE

===Tuesday===
->DONE

===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE