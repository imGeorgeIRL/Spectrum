INCLUDE globals.ink

{Day_Of_Week:
- 0: { calendar_Interactions > 0: -> MondayMain | -> Monday_First_choice }
- 1: { calendar_Interactions > 1: -> TuesdayMain | -> Tuesday_First_Choice }
- 2: { calendar_Interactions > 2: -> WednesdayMain | -> Wednesday_First_Choice }
- 3: Thursday
- 4: Friday
}


//{ calendar_Interactions > 0: -> MondayMain | -> Monday_First_choice }

//MONDAY
=== MondayMain ===
better check my planner #speaker:Austin
I wonder where I left it?
-> END


=== Monday_First_choice ===
    First day of uni #speaker:Austin
    feeling a little...
    * [Nervous]
        I'm sure it'll go okay though!
        ~ calendar_Interactions = calendar_Interactions + 1
        -> MondayMain
    * [Excited]
        I can't wait to start!
        ~ calendar_Interactions++
        -> MondayMain
    * [Terrified]
        Theres gonna be so many people there!
        <shake> eek! </shake>
        ~ calendar_Interactions++
        -> MondayMain



//TUESDAY

=== TuesdayMain ===
Gotta check my planner again,#speaker:Austin
I think it's in the same spot as yesterday.
->END

=== Tuesday_First_Choice ===
Day two! #speaker:Austin
Hopefully things go well today,
That Noah guy is 
    * interesting
        to say the least.
        ~calendar_Interactions++
        ->TuesdayMain
    * nice 
        hopefully he stays that way.
        ~calendar_Interactions++
        ->TuesdayMain
    * funny
        but I'm sure he knows that already...
        ~calendar_Interactions++
        ->TuesdayMain
        



//WEDNESDAY 
=== WednesdayMain ===
I need to check my planner,#speaker:Austin
I think I have some things to do today.
-> END

=== Wednesday_First_Choice ===
Man I am so tired,#speaker:Austin
yesterday was awful
    * at least I have no class today
        although its not like I has class yesterday...
        ~calendar_Interactions++
        ->WednesdayMain
    * Maybe I should apoligise to Noah
        I probably freaked him out yesterday...
        ~calendar_Interactions++
        ->WednesdayMain
    * I feel like I haven't slept at all 
        even though I slept for like 12 hours...
        ~calendar_Interactions++
        ->WednesdayMain
        
    
        
        
        