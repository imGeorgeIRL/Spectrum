INCLUDE globals.ink 

Is it this bus? #speaker:Austin
    *[Yes]
        I found it!
        ~ makeChoice(1)
        
    *[No]
        wait, no it is!
        ~ makeChoice(1)
        
- here it comes!
->DONE 