* [Start] -> d1_intro

=== d1_intro ===
A new delivery has arrived.

BODY 1:
22-year woman, passed away under unspecified circumstances in an alley next to a health food store.

BODY 2:
A teenage boy harvested from the local morgue. Acoording to the death report, he died after ingesting a Tide pod.

BODY 3:
Male, early thirties, retrieved from a local bar after starting a fight and getting knocked out.

    * [OK] -> d1_1

=== d1_1 ===
A male vampire with tangled hair, dark circles under his eyes, and sickly-green skin shows up.

(Backstory: moving from place to place, on the run from the police for assaulting someone for blood. Accidentally touched a bible when tucking into bed, now is sick. 
Personality: paranoid and anxious.)

VAMPIRE:
I'm so sick, man. Is this... the right place?
    * [There is a hospital two blocks away.] -> d1_1_hospital
    * [What happened?] -> d1_1_story
    
=== d1_1_hospital ===
YOU:
There is a hospital two blocks away. Dr. Nielsen works night shifts and sees patients with... blood-related issues.

VAMPIRE:
Yeah, that's a no-go, man. I'm trying to stay off the grid, if you know what I'm saying.

    * [Tell me what happened.] -> d1_1_story

=== d1_1_story ===
YOU:
If you tell me what happened, perhaps I could help?

VAMPIRE:
Where do I start? Let's just say I had a little... altercation. Nothing too bad, but now some people are looking for me. I've been on the run for the past week, jumping from one motel to another. So earlier today I checked into this motel not far from here, right? I was tired, not really paying attention to my surroundings, and I crashed on this dingy couch, and what I didn't notice is there was a bible on the couch. I mean, who puts a fucking bible on a couch? I got violently sick after this, and I'm just... not feeling well, you know. So I just came here because I'd heard of the place, I didn't know what else to do, and I figured I could use a drink.

    * [I may have just what you need.] -> d1_choose_body

=== d1_choose_body ===

    * [Body 1] -> d1_1_body1
    * [Body 2] -> d1_1_body2
    * [Body 3] -> d1_1_body3
    
=== d1_1_body1 ===
VAMPIRE:
Ooh, that hit the spot. I'm feeling better already. Thanks man!

YOU:
You're welcome. When you need some more good stuff, do come back.

VAMPIRE:
Nah, I don't think I'm sticking around. Need to get a move on, what with the fucking cops- I mean, nevermind. I gotta go now. Bye!
    * [Next client] -> d1_2

=== d1_1_body2 ===
VAMPIRE:
Blaargh... what the hell is this stuff? Are you trying to poison me? Fuck you man, I'm outta here.
    * [Next client] -> d1_2
=== d1_1_body3 ===
VAMPIRE:
This... is not- hic!- too bad. It's been a while since I had a proper drink. All because of those fucking coppers. You know what, I'm done with this bullshit! I'm not running anymore. I'm going to go and... settle the scores. 
    * [Next client] -> d1_2
=== d1_2 ===

-> END