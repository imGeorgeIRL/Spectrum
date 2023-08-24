INCLUDE globals.ink

{clothesChosen:
- 0: ->First
- 1: ->Second
- 2: ->Third
- 3: ->Fourth
- 4: ->Choose
}

===Choose===
I can't choose any more #speaker: Austin
its too much money!
->DONE

===First===
Do I want this one? I can afford three shirts#speaker: Austin
    *yes
        It's nice
        ~clothesChosen = 1
        ~waitTime(1)
        ->DONE
    *no 
        I'll keep looking
        ~waitTime(1)
        ->DONE


===Second===
Do I want this one? I can pick two more #speaker: Austin
    *yes
        It's nice
        ~clothesChosen = 2
        ~waitTime(1)
        ->DONE
    *no 
        I'll keep looking
        ~waitTime(1)
        ->DONE


===Third===
Do I want this one? I can pick one more #speaker: Austin
    *yes
        It's nice
        ~clothesChosen = 3
        ~waitTime(1)
        ->DONE
    *no 
        I'll keep looking
        ~waitTime(1)
        ->DONE

===Fourth===
Do I want this one? This is my last choice #speaker: Austin
    *yes
        It's nice
        ~clothesChosen = 2
        ~waitTime(1)
        ->DONE
    *no 
        I'll keep looking
        ~waitTime(1)
        ->DONE