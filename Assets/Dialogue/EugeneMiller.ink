INCLUDE globals.ink 

{Day_Of_Week:
- 0: -> Monday 

}

===Monday===
{isLateToClass : ->MondayLATE}
{spokenToMiller : ->Monday2}
Hi, what can I do you for? #speaker: Miller
Hi sir #speaker: Austin
I just wanted to talk to you about something
yes? #speaker: Miller
I um..#speaker: Austin
I have a disability
okay?#speaker: Miller
its Autism#speaker: Austin
right...#speaker: Miller
so I just wanted to tell you,#speaker: Austin
just in case you think im just...
weird and offputting.
Yeah, no worries kid,#speaker: Miller
I'll keep it in mind...?#speaker: Austin
Just take a seat over there by the door
~drMiller(1)
~spokenToMiller = true
thank you. #speaker: Austin
    ->DONE

===MondayLATE===
{spokenToMiller : ->Monday2Late}
You're late. #speaker: Miller
    * [I'm so sorry, I caught the wrong bus]
        Are you new in town? #speaker: Miller
        yes sir, #speaker: Austin
        okay, i'll let it slide just this once. #speaker: Miller
    * [apologies sir, it wont happen again im so sorry]
        You can just call me Miller #speaker: Miller
        and its fine,
        shoot me an email next time though.
    * [sorry about that, did I miss anything important?]
        would you count the roll as important? #speaker: Miller
            ** [not really?]
                then not really. #speaker: Miller
            ** [sorta??]
                then sorta. #speaker: Miller
    - You can go take a seat over by the door #speaker: Miller
    ~drMiller(1)
    ~spokenToMiller = true
thank you #speaker: Austin
    ->DONE
    
===Monday2===
You can just take a seat over there, #speaker: Miller
class is about to start.
->DONE

===Monday2Late===
Just take a seat over by the door, #speaker: Miller
class has already started.
->DONE


