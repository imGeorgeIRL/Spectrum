INCLUDE globals.ink

{ calendar_Interactions > 0: -> Main | -> First_choice }
=== Main ===
better check my planner
I wonder where I left it?
-> END


=== First_choice ===
    First day of uni 
    feeling a little...
    * [Nervous]
        I'm sure it'll go okay though!
        ~ calendar_Interactions = calendar_Interactions + 1
        -> Main
    * [Excited]
        I can't wait to start!
        ~ calendar_Interactions++
        -> Main
    * [Terrified]
        Theres gonna be so many people there!
        <shake> eek! </shake>
        ~ calendar_Interactions++
        -> Main


