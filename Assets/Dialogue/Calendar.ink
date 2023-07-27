INCLUDE globals.ink

{Day_Of_Week:
- 0: -> Monday
- 1: -> Tuesday
- 2: -> Wednesday
- 3: -> Thursday
- 4: -> Friday
}

//MONDAY



=== Monday===
{ calendar_Interactions == 1: ->Monday2 }
    First day of uni #speaker:Austin
    feeling a little...
    * [Nervous]
        I'm sure it'll go okay though!
        ~ calendar_Interactions = 1
        -> Monday2
    * [Excited]
        I can't wait to start!
        ~ calendar_Interactions = 1
        -> Monday2
    * [Terrified]
        Theres gonna be so many people there!
        <shake> eek! </shake>
        ~ calendar_Interactions = 1
        -> Monday2

=== Monday2 ===
better check my planner #speaker:Austin
I wonder where I left it?
~calendarInteract(1)
-> END

//TUESDAY

=== Tuesday2 ===
Gotta check my planner again,#speaker:Austin
I think it's in the same spot as yesterday.
~calendarInteract(1)
->END

=== Tuesday ===
{ calendar_Interactions == 1: ->Tuesday2 }
Day two! #speaker:Austin
Hopefully things go well today,
That Noah guy is 
    * interesting
        to say the least.
        ~calendar_Interactions = 1
        ->Tuesday2
    * nice 
        hopefully he stays that way.
        ~calendar_Interactions = 1
        ->Tuesday2
    * funny
        but I'm sure he knows that already...
        ~calendar_Interactions = 1
        ->Tuesday2
        



//WEDNESDAY 
=== Wednesday2 ===
I need to check my planner,#speaker:Austin
I think I have some things to do today.
-> END

=== Wednesday ===
{calendar_Interactions == 1: ->Wednesday2 }
Man I am so tired,#speaker:Austin
yesterday was awful
    * at least I have no class today
        although its not like I has class yesterday...
        ~calendar_Interactions = 1
        ->Wednesday2
    * Maybe I should apoligise to Noah
        I probably freaked him out yesterday...
        ~calendar_Interactions = 1
        ->Wednesday2
    * I feel like I haven't slept at all 
        even though I slept for like 12 hours...
        ~calendar_Interactions = 1
        ->Wednesday2
        
    

===Thursday===
->DONE

===Friday===
->DONE   
        
        