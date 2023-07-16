INCLUDE globals.ink 

{Day_Of_Week:
- 0: -> Monday 
- 1: -> Tuesday
- 2: -> Wednesday
- 3: -> Thursday
- 4: -> Friday
}

===Monday===
{isLateToClass : ->MondayLATE}
~ dailyTasks(2)
Hi, what can I do you for? #speaker: Miller
Hi sir #speaker: Austin
I just wanted to talk to you about something
yeah, whats up kid? #speaker: Miller
I'm autistic #speaker: Austin
Alright, what can I do to accommodate? #speaker: Miller
Just sort of explaning things very clearly, #speaker: Austin
allowing me to wear headphones in class if I need them, 
having access to lecture notes.
yeah of course kiddo, #speaker: Miller
let me know if you need anything else
and just take a seat over there by the door
thank you. #speaker: Austin
    ->DONE

===MondayLATE===
~ dailyTasks(2)
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
    ->DONE

===Tuesday===
->DONE

===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE
