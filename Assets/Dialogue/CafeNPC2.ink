INCLUDE globals.ink 

{Day_Of_Week:
- 0: -> Monday
- 1: -> Tuesday
- 2: -> Wednesday
- 3: -> Thursday
- 4: -> Friday
}

===Monday===
um... hi? #speaker: Guy 
hi #speaker: Austin 
can I help you? #speaker: Guy 
    * do you know which bus takes me to Cygnus University? # speaker: Austin 
        oh you go to cygnus?! #speaker: Guy 
        yeah, first day today and im pretty sure im late #speaker: Austin 
        lucky for you, I go there too #speaker: Guy 
        you wanna catch the 838 from that bus stop over there #speaker: Austin 
        Thank you!
        no worries, good luck pal #speaker: Guy 
        ~spokeTo(1)
        ~FoundBus = true
    - you too! #speaker:Austin
    wait..
    ->DONE

===Tuesday===
->DONE

===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE
