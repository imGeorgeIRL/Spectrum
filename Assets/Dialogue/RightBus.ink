INCLUDE globals.ink 

Is it this bus? #speaker:Austin
    *[Yes]
        I found it!
        ~busChosen(1)
        
    *[No]
        wait, no it is!
        ~busChosen(1)
        
- here it comes!
->DONE 