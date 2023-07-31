INCLUDE globals.ink

->Main
===Main===
{orderedDrink : ->Ordered | ->Ordering}

=Ordering
Hi, what can I get for you? #speaker: Barista #size:small
oh um, can I get a #speaker:Austin #size:normal
    *Flat White 
        ~drink = "flat white"
        ~cost = 4.5
    *Hot Chocolate
        ~drink = "hot chocolate"
        ~cost = 4.5
    *Peppermint Tea
        ~drink = "peppermint tea"
        ~cost = 5
- what size? #speaker: Barista #size:small
pardon? #speaker:Austin #size:normal
what size?! #speaker: Barista #size:normal
    Oh! sorry, I'll get a #speaker:Austin #size:normal
    *small
        ~size = "tall"
        Its called a tall #speaker: Barista #size:small
        sorry? #speaker:Austin #size:normal
        a small drink #speaker: Barista #size:small
        its a "tall"
    *medium
        ~size = "grande"
        Its called a grande #speaker: Barista #size:small
        sorry? #speaker:Austin #size:normal
        a medium drink #speaker: Barista #size:small
        its a "grande" 
    *large
        ~size = "venti"
        Its called a venti #speaker: Barista #size:small
        sorry? #speaker:Austin #size:normal
        a large drink #speaker: Barista #size:small
        its a "venti"
    - right #speaker:Austin #size:normal
is that all for today? #speaker: Barista #size:small
I'm sorry, I really can't hear you #speaker:Austin #size:normal
I said <shake a=0.5> is that all for you today?!</shake> #speaker: Barista #size:big
Oh, yeah, thank you #speaker:Austin #size:normal
which table are you at? #speaker: Barista #size:small
the one just to my left, #speaker:Austin #size:normal
so one {size}, {drink} will come to {cost} dollars #speaker: Barista #size:small
thank you #speaker:Austin #size:normal
~orderedDrink = true
->DONE 

=Ordered
I'll bring your drink to you when it's ready #speaker: Barista #size:small
->DONE

->DONE