INCLUDE globals.ink

{clothesChosen:
- 0: ->First
- 1: ->Second
- 2: ->Third
- 3: ->Choose
}

===Choose===
I can't choose any more #speaker: Austin
its too much money!
~waitTime(1)
->DONE

===First===
Do I want this one? I can afford three shirts#speaker: Austin
    *yes
        It's nice
        ~clothesChosen = 1
        ~costOfClothes += 19.99
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
        ~costOfClothes += 19.99
        ~waitTime(1)
        ->DONE
    *no 
        I'll keep looking
        ~waitTime(1)
        ->DONE


===Third===
Do I want this one? This is my last choice #speaker: Austin
    *yes
        It's nice
        ~clothesChosen = 3
        ~costOfClothes += 19.99
        ~waitTime(1)
        ->DONE
    *no 
        I'll keep looking
        ~waitTime(1)
        ->DONE

