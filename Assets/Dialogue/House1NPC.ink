INCLUDE globals.ink 
{donnyInteractions:
- 0: ->Monday
- 1: {houseNPC1: ->Monday2 | -> Tuesday}
- 2: {houseNPC1: ->Tuesday2 | -> Wednesday}
- 3: ->Wednesday2
}

===Monday===
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

===Tuesday2===
Have a stellar day kid! #speaker:Donny
hehe, "stellar" #speaker:Austin
->DONE

//WEDNESDAY
===Wednesday===
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

===Wednesday2===
Chin up kid, #speaker: Donny
can't see the stars looking down #speaker:Austin
unless im looking into a telescope,
or a...pool of water...
okay...sure #speaker: Donny
->DONE

