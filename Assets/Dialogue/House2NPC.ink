INCLUDE globals.ink

{Day_Of_Week:
- 0: ->Monday
- 1: -> Tuesday
- 2: -> Wednesday
- 3: -> Thursday
- 4: -> Friday
}

===Monday===
{houseNPC2 : ->Monday2 }
<shake>Hey you!</shake> what are you doing in front of my house?! #speaker:Charles
Who? Me?#speaker:Austin
Yes you!!#speaker:Charles
I'm walking to the bus stop#speaker:Austin
is...is that allowed?
hrmmph...#speaker:Charles
I guess...
Curse the day this good for nothing town built that bus stop!
~spokeTo(1)
~houseNPC2 = true
sorry...?#speaker:Austin
->DONE


===Monday2===
Best you keep walking kid #speaker:Charles
sorry! #speaker:Austin
->DONE


===Tuesday===
->DONE

===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE