open System
open System.Threading
open Nancy
open Nancy.Hosting.Self

module BigNumberService =

  let seed = 100000000

  type BigNumberModule() as self =
    inherit NancyModule()
    do 
      self.Get.["/givemeanumber/{id}"] <-
        fun x ->
          let id = (((x :?> DynamicDictionary).Item "id") :?> DynamicDictionaryValue).Value :?> string |> int
          Thread.Sleep(1000 + id * 50)
          sprintf "I choose %d" (seed + (id % 3)) :> obj

[<EntryPoint>]
let main argv = 
    let uri = "http://localhost:9006/"
    let nancyHost = new NancyHost(new Uri(uri))
    nancyHost.Start()
    printfn "Service started..."
    printfn "go to %s/givemeanumber/{n} where n is an integer to get a number" uri
    printf "Press any key to stop"
    Console.ReadKey() |> ignore
    nancyHost.Stop()
    0
