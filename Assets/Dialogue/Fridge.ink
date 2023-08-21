INCLUDE globals.ink 

{Day_Of_Week:
- 0: {isNight: ->NightTime | ->Monday}
- 1: {isNight: ->TuesdayNight | ->Tuesday }
- 2: -> Wednesday
- 3: -> Thursday
- 4: -> Friday
}


=== Monday ===
{fridge_Interactions == 1: -> AlreadyDone | ->MondayNotEaten}
=MondayNotEaten
what should I eat for breakfast?#speaker:Austin
    * Cereal
        which cereal?
            ** Fruity loops
                 So tasty!
                ~ fridge_Interactions = 1
                -> DONE
            ** Corn Flakes
                 okay I suppose...
                ~ fridge_Interactions = 1
                -> DONE
    * Toast
         <dangle a=0.5> yummy toast!</dangle>
        ~ fridge_Interactions = 1
        -> DONE
    * Nothing
         Not feeling that hungry this morning...
        ~ fridge_Interactions = 1
        -> DONE

        
=== AlreadyDone ===
hmm, i've already eaten...#speaker:Austin
-> DONE


//*************************************************


==NightTime===
{hasEatenDinner: ->isTrue | ->notTrue }

    =isTrue
    I've already had dinner#speaker:Austin
    Maybe I could watch some tv now?
    ->DONE


    =notTrue
        What should I eat for dinner?#speaker:Austin
            *[Cereal]
                I know its not that great for me
                but I'm super tired.
                ~hasEatenDinner = true
            
            *[Mac and Cheese]
                I'm almost out,
                I should go shopping sometime this week.
                ~hasEatenDinner = true
            
            *[Toast]
                I love toast
                I could eat it for every meal!
                ~hasEatenDinner = true
            
        -I didn't even realise I was that hungry
        ->DONE
->DONE

//TUESDAY ---------------------------------
===Tuesday===
{ isNight : ->TuesdayNight }
{ fridge_Interactions == 1 : ->Tuesday2 | ->Tuesday1}

=Tuesday1
what should I eat for breakfast?#speaker:Austin
    * [Cereal]
        which cereal?
            ** [Fruity loops]
                 So tasty!
                ~fridge_Interactions = 1
                ->DONE
            ** [Corn Flakes]
                 okay I suppose...
                ~ fridge_Interactions = 1
                ->DONE
    * [Toast]
         <dangle a=0.5> yummy toast!</dangle>
        ~fridge_Interactions = 1
        ->DONE
    * [Nothing]
         Not feeling that hungry this morning...
        ~fridge_Interactions = 1
        ->DONE



===Tuesday2===
I've already had breakfast, #speaker:Austin
I should probably head off soon!
->DONE

===TuesdayNight===
I should really eat something #speaker:Austin
but I think I just want to go to sleep.
I'm so <dangle a=0.5>exhausted</dangle> from today.
->DONE

===Wednesday===
{ fridge_Interactions == 1 : ->Wednesday2 | ->Wednesday1}

    =Wednesday1
    Breakfast time, what should I have today? #speaker: Austin
    *[Cereal]
        Which one?
            ** [Fruity loops]
                oh, I'm out
                guess i'll have corn flakes instead...
                ~ fridge_Interactions = 1
                ->DONE
            ** [Corn Flakes]
                They're kinda stale...
                did I forget to close it?
                ~ fridge_Interactions = 1
                ->DONE
    *[Toast]
        is that...mould on my bread?
        I'll just pick it off
        I think thats okay...
        ~ fridge_Interactions = 1
        ->DONE


    =Wednesday2
    ->DONE
->DONE

===Thursday===
->DONE

===Friday===
->DONE