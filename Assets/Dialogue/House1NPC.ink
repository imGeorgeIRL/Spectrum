INCLUDE globals.ink 

{Day_Of_Week:
- 0: ->Monday
- 1: -> Tuesday
- 2: -> Wednesday
- 3: -> Thursday
- 4: -> Friday
}

===Monday===
{houseNPC1 : ->Monday2 }
Good morning son! #speaker:Mr. Donaldson
Good morning sir #speaker:Austin
Please, call me Donny #speaker: Donny
Okay sir #speaker: Austin
~spokeTo(1)
~houseNPC1 = true
<shake a=0.7> I mean Donny!! </shake>
->DONE

===Monday2===
Heading to class?#speaker:Mr. Donaldson
Yeah#speaker:Austin
->DONE

===Tuesday===
{houseNPC1: ->Tuesday2 }
Morning Austin! #speaker:Mr. Donaldson
Hi Donny #speaker:Austin
Hey! you remembered! #speaker:Mr. Donaldson
yeah haha #speaker:Austin
Have a good one kid #speaker:Mr. Donaldson
you too. #speaker:Austin
~spokeTo(1)
~houseNPC1 = true
->DONE

=Tuesday2
Have a stellar day kid! #speaker:Mr. Donaldson
->DONE

===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE
