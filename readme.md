# Unity Decorator Bug

After upgrading to a newer version of Unity we found that our process started to fail mysteriously with `StackOverflowException`. 

What changed is the behavior of declarations of that look like
`RegisterType<TInterface, TClass>(new InjectionFactory...)`

This project explores the behavior of such declarations in different versions of Unity. Results:

| UNITY Version | Behavior |
| ------------- | -------- |
| <= 5.2.0 | InjectionFactory used |
| 5.2.1 ... 5.6.1 | InjectionFactory ignored, potential stack overflow |
| >= 5.7.0 | InvalidOperationException |

More at <https://ikriv.com/blog/?p=2539>.