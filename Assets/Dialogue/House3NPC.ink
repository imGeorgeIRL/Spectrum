INCLUDE globals.ink 

{sanjayInteractions:
- 0: ->Monday
- 1: {houseNPC3: ->Monday2 | -> Tuesday}
- 2: {houseNPC3: ->Tuesday2 | -> Wednesday}
- 3: ->Wednesday2
}

===Monday===
Just enjoying the morning #speaker:Sanjay
I think I saw a kookaburra up in that tree
He's laughing at me....
~spokeTo(1)
~houseNPC3 = true
~sanjayInteractions = 1
ok... #speaker:Austin
cool.
->DONE

===Monday2===
I'm gonna catch that bird one day! #speaker:Sanjay
okay... #speaker:Austin
->DONE

===Tuesday===
He's still laughing at me #speaker:Sanjay
always sitting up there 
ha 
ha 
ha 
... #speaker:Austin
~houseNPC3 = true
~sanjayInteractions = 2
->DONE

===Tuesday2===
Are you okay? #speaker:Austin
ha #size:small #speaker:Sanjay
haha
laughing at me #speaker:Sanjay #size:normal
->DONE 

===Wednesday===
Oh, hey, youre new! #speaker:Sanjay
what? #speaker:Austin
I havent seen you around before #speaker:Sanjay
What's your name? #speaker:Austin
It- im- huh? We've spoken for the past two days
Really? I think I would remember that  #speaker:Sanjay
You kept talking about this kookaburra #speaker:Austin
OH! Yeah sorry, I get a little fixated on things #speaker:Sanjay
autism and all haha 
wait you're autistic? #speaker:Austin
yeah...and? #speaker:Sanjay
I'm autistic #speaker:Austin
my name's austin.
Your name is one letter off autism. #speaker:Sanjay
what? #speaker:Austin
yeah, change the "n" to an "m" #speaker:Sanjay
rearrange a few letters
spells autism.
oh, youre right... strange #speaker:Austin
yeah, I wonder if whoever named you did that on purpose #speaker:Sanjay
hmm... #speaker:Austin
~houseNPC3 = true
~sanjayInteractions = 3
->DONE

===Wednesday2===
We should talk more later #speaker:Sanjay
I can tell you all about birds 
and Ill tell you all about space #speaker:Austin
sounds...out of this world! #speaker:Sanjay
haha, thats...funny #speaker:Austin
I know right! #speaker:Sanjay
->DONE

