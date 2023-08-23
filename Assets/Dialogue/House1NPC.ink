INCLUDE globals.ink 

{Day_Of_Week:
- 0: ->Monday
- 1: -> Tuesday
- 2: -> Wednesday
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
~donnyInteractions = 1
->DONE

===Monday2===
Heading to class?#speaker:Mr. Donaldson
Yeah#speaker:Austin
->DONE

//TUESDAY
===Tuesday===
{donnyInteractions == 0: ->Monday}
{houseNPC1: ->Tuesday2 }
Morning Austin! #speaker: Donny
Hi Donny #speaker:Austin
Hey! you remembered! #speaker: Donny
yeah haha #speaker:Austin
Have a good one kid #speaker: Donny
you too. #speaker:Austin
~spokeTo(1)
~houseNPC1 = true
~donnyInteractions = 2
->DONE

=Tuesday2
Have a stellar day kid! #speaker: Donny
->DONE

//WEDNESDAY
===Wednesday===
{donnyInteractions == 0: ->Monday}
{donnyInteractions == 1: ->Tuesday}
{houseNPC1: ->Wednesday2 }
Hey Austin, #speaker: Donny
Hi Donny #speaker:Austin
how's it going champ? #speaker: Donny
Well, the universe is expanding at an excellerating rate #speaker:Austin
and uhh, dark matter might not be as dark as we think
but aside from that,
im not doing too great.
Oh, well you seem like a smart fella #speaker: Donny
so i'm sure you'll work through it!
Probably not, #speaker:Austin
but i appreciate the vote of confidence.
~spokeTo(1)
~houseNPC1 = true
~donnyInteractions = 3
->DONE

=Wednesday2
Chin up kid, #speaker: Donny
can't see the stars looking down #speaker:Austin
unless im looking into a telescope,
or a...pool of water...
okay...sure #speaker: Donny
->DONE

