INCLUDE globals.ink

{fridge_Interactions > 0: -> AlreadyDone | ->Main}
=== Main ===
what should I eat for breakfast?
    * [Cereal]
        which cereal?
            ** [Fruity loops]
                 So tasty!
                ~ fridge_Interactions++
                -> DONE
            ** [Corn Flakes]
                 okay I suppose...
                ~ fridge_Interactions++
                -> DONE
    * [Toast]
         <dangle a=0.5> yummy toast!</dangle>
        ~ fridge_Interactions++
        -> DONE
    * [Nothing]
         Not feeling that hungry this morning...
        ~ fridge_Interactions++
        -> DONE

        
=== AlreadyDone ===
hmm, i've already eaten...
-> DONE