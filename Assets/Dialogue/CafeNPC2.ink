INCLUDE globals.ink 

{Day_Of_Week:
- 0: -> Monday
- 1: {cafeOnMonday : ->Tuesday | -> Monday }
- 2: -> Wednesday
- 3: -> Thursday
- 4: -> Friday
}

===Monday===
{townNPC2 : ->Monday2 }
um... hi? #speaker: Guy 
hi #speaker: Austin 
can I help you? #speaker: Guy 
    * do you know which bus takes me to Cygnus University? # speaker: Austin 
        oh you go to cygnus?! #speaker: Guy 
        yeah, {Day_Of_Week == 0: second day | first day }  today and im pretty sure im late #speaker: Austin 
        lucky for you, I go there too #speaker: Guy 
        you wanna catch the 838 from that bus stop over there #speaker: Austin 
        Thank you!
        no worries, good luck pal #speaker: Guy 
        ~spokeTo(1)
        ~FoundBus = true
        ~townNPC2 = true
    - you too! #speaker:Austin
    wait..
    ->DONE

===Monday2===
Remember, but 838 #speaker: Guy 
you should really write that down somewhere!
->DONE

===Tuesday===
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
->DONE
=Tuesday2
<wave a=0.2>"838, 838, thats the bus you need to take!"</wave> #speaker: Guy 
->DONE

===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE
