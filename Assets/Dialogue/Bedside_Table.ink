INCLUDE globals.ink

{Day_Of_Week:
- 0: -> Monday
- 1: -> Tuesday
- 2: -> Wednesday
- 3: -> Thursday
- 4: -> Friday
}

-> main
{ bedside_Interactions > 0: ->DONE | ->main}
=== main ===

-> DONE


===Monday===
{isNight : ->MondayNight }
Oh here it is!#speaker:Austin
whats on the agenda today?
breakfast, uni, make a friend
should be easy enough
maybe...
->DONE

=MondayNight
What do I have tomorrow?
Uni again,
{isLateToClass : I should catch the right bus this time}
I'll try and talk to Noah again as well
dialogue 
->DONE





===Tuesday===
->DONE

===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE