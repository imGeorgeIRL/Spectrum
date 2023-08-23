INCLUDE globals.ink 

{Day_Of_Week:
- 0: -> Monday
- 1: {cafeOnMonday : ->Tuesday | -> Monday }
- 2: -> Wednesday
}

===Monday===
{townNPC2 : ->Monday2 }
um... hi? #speaker: Guy 
hi #speaker: Austin 
can I help you? #speaker: Guy 
    * do you know which bus takes me to Cygnus University? # speaker: Austin 
        oh you go to cygnus?! #speaker: Guy 
        yeah, but im pretty sure im late #speaker: Austin 
        lucky for you, I go there too #speaker: Guy 
        you wanna catch the 838 from that bus stop over there #speaker: Austin 
        Thank you!
        no worries, good luck pal #speaker: Guy 
        ~spokeTo(1)
        ~FoundBus = true
        ~townNPC2 = true
    - you too! #speaker:Austin
    wait..
    ~town2 = 1
    ->DONE

===Monday2===
Remember, but 838 #speaker: Guy 
you should really write that down somewhere!
->DONE

===Tuesday===
{town2 == 0: ->Monday}
{townNPC2: ->Tuesday2 | ->Tuesday1}

=Tuesday1
Oh it's you again! #speaker: Guy 
Cygnus kid!
Yeah, thats me #speaker:Austin
cygnus kid who forgot which bus he needed to catch
Thats alright, just write it down somewhere #speaker: Guy 
It's written on a note on my fridge #speaker:Austin
but somehow I still forget
oh...I dont know how to help you then #speaker: Guy 
just remember, <b>838</b>
Turn it into a song or something
<wave a=0.2>"838, 838, thats the bus you need to take!"</wave>
        ~spokeTo(1)
        ~FoundBus = true
        ~townNPC2 = true
        ~town2 = 1
->DONE
=Tuesday2
<wave a=0.2>"838, 838, thats the bus you need to take!"</wave> #speaker: Guy 
->DONE

===Wednesday===
{town2 == 0: ->Monday}
{town2 == 1: ->Tuesday}
Oh man, you're not late again are you? #speaker: Guy 
nope, no class today #speaker:Austin
I just wanted to come over here and say hi
so...hi
hi haha #speaker: Guy 
You know, I've been meaning to ask
what's that on your tshirt?
Its Saturn! #speaker:Austin
Did you know that saturn isn't the only planet with rings?
Jupiter, Uranus and neptune all have ring systems!
Saturn's rings are actually really thin, 
They have an average thickness of 10 metres!
but the whole ring system is almost 500 000 metres wide!
and- sorry, I'm rambling
No no, you're fine #speaker: Guy 
I can tell you're intrested in it!!
I'm here most mornings
so if you ever wanna come infodump to me, feel free!
wow! thank you! #speaker:Austin
Take care bud #speaker: Guy 
you too! wait- #speaker:Austin
what's your name?
oh, it's Guy! #speaker: Guy 
cool! #speaker:Austin
~town2 = 3
->DONE


