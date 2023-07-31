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
{houseNPC2: ->Tuesday2 }
I'm keeping my eyes on you kid #speaker:Charles
you youngins are all the same 
destructive and loud
I'm not #speaker:Austin
what? #speaker:Charles
I'm not destructive, and im certainly not loud #speaker:Austin
yeah well we'll see about that. #speaker:Charles
~spokeTo(1)
~houseNPC2 = true
->DONE

=Tuesday2
hrmmph #speaker:Charles
->DONE

===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE