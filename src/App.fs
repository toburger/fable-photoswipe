module fable_photoswipe

open Fable.Core
open JsInterop
open Fable.Import.Browser
open Fable.Import.PhotoSwipe

importSideEffects "photoswipe/dist/photoswipe.css"
importSideEffects "photoswipe/dist/default-skin/default-skin.css"
let photoSwipe: PhotoSwipeStatic = importDefault "photoswipe/dist/photoswipe"
let photoSwipeUIDefault: PhotoSwipe.UI<PhotoSwipe.Options> = importDefault "photoswipe/dist/photoswipe-ui-default"

let createPhotoItem src width height =
    let item = createEmpty<PhotoSwipe.Item>
    item.src <- src
    item.w <- float width
    item.h <- float height
    item

let init() =
    let element = document.getElementById("pswp")
    let items =
        [ createPhotoItem "https://farm2.staticflickr.com/1043/5186867718_06b2e9e551_b.jpg" 964 1024
          createPhotoItem "https://farm7.staticflickr.com/6175/6176698785_7dee72237e_b.jpg" 1024 683
          createPhotoItem "https://farm6.staticflickr.com/5023/5578283926_822e5e5791_b.jpg" 1024 768
          createPhotoItem "https://farm3.staticflickr.com/2567/5697107145_a4c2eaa0cd_o.jpg" 1024 1024 ]
        |> ResizeArray
    let options = createEmpty<PhotoSwipe.Options>
    options.history <- Some false
    options.focus <- Some false
    options.showAnimationDuration <- Some 0.0
    options.hideAnimationDuration <- Some 0.0
    let gallery = photoSwipe.Create(element, U2.Case1 (box photoSwipeUIDefault), items, options)
    gallery.init()

init()