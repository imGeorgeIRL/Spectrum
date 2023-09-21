INCLUDE globals.ink

{charlesInteractions:
- 0: ->Monday
- 1: {houseNPC2: ->Monday2 | -> Tuesday}
- 2: {houseNPC2: ->Tuesday2 | -> Wednesday}
- 3: ->Wednesday2
}

===Monday===
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
~charlesInteractions = 1
->DONE


===Monday2===
Best you keep walking kid #speaker:Charles
sorry! #speaker:Austin
->DONE


===Tuesday===
I'm keeping my eyes on you kid #speaker:Charles
you youngins are all the same 
destructive and loud
I'm not #speaker:Austin
what? #speaker:Charles
I'm not destructive, and im certainly not loud #speaker:Austin
yeah well we'll see about that. #speaker:Charles
~spokeTo(1)
~houseNPC2 = true
~charlesInteractions = 2
->DONE

===Tuesday2===
hrmmph #speaker:Charles
->DONE

===Wednesday===
Mr. Charles? #speaker:Austin
Huh? Oh it's you, #speaker:Charles
why do you keep bothering me?
bothering you? #speaker:Austin
Yes, you keep talking to me, #speaker:Charles
and never about anything interesting.
What do you find interesting? #speaker:Austin
stars, space, and football. #speaker:Charles
I love the first two, football not so much #speaker:Austin
...
Mr charles, did you know
that they recently found an exoplanet
in the habitable zone of it's star?
and it's amazing because
are you done yet? #speaker:Charles
huh? #speaker:Austin
I said I liked them, not that I wanted to talk about them #speaker:Charles
now scram.
oh, im sorry. #speaker:Austin
~spokeTo(1)
~houseNPC2 = true
~charlesInteractions = 3
->DONE

===Wednesday2===
If you so much as mention space #speaker:Charles
I'll start screaming
<shake>scram!</shake>
->DONE
