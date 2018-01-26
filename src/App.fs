module fable_photoswipe

open Fable.Core
open Fable.PowerPack
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

type Photo =
    { created: System.DateTime
      filename: string
      gallery: string
      height: int
      width: int }

let getGallery name =
    Fetch.fetch (sprintf "http://raspberrypi:8083/%s" name) []
    |> Promise.bind (fun res -> res.json<seq<Photo>>())
    |> Promise.map (Seq.map (fun photo ->
        let url = sprintf "http://raspberrypi:8083/gallery/%s/%s" photo.gallery photo.filename
        createPhotoItem url photo.width photo.height))

// let init() = promise {
//     let element = document.getElementById("pswp")
//     let! photos = getGallery "latest"
//     // let items =
//     //     [ createPhotoItem "https://farm2.staticflickr.com/1043/5186867718_06b2e9e551_b.jpg"  964 1024
//     //       createPhotoItem "https://farm7.staticflickr.com/6175/6176698785_7dee72237e_b.jpg" 1024  683
//     //       createPhotoItem "https://farm6.staticflickr.com/5023/5578283926_822e5e5791_b.jpg" 1024  768
//     //       createPhotoItem "https://farm3.staticflickr.com/2567/5697107145_a4c2eaa0cd_o.jpg" 1024 1024 ]
//     //     |> ResizeArray
//     let options = createEmpty<PhotoSwipe.Options>
//     options.history <- Some false
//     options.focus <- Some false
//     options.showAnimationDuration <- Some 0.0
//     options.hideAnimationDuration <- Some 0.0
//     let gallery = photoSwipe.Create(element, U2.Case1 (box photoSwipeUIDefault), ResizeArray photos, options)
//     gallery.applyZoomPan(10.0, 0.0, 0.0)
//     gallery.init()
//     return ()
// }

let el = document.getElementById("pswp")

let log x =
    console.log x
    x

let showGallery galleryName idx = promise {
    let! photos = getGallery galleryName
    let options = createEmpty<PhotoSwipe.Options>
    options.index <- Some 0.0
    options.maxSpreadZoom <- Some 4.0
    options.galleryUID <- Some (float (hash galleryName))
    options.showHideOpacity <- Some true
    options.getThumbBoundsFn <- Some (fun index ->
        match idx with
        | None -> null
        | Some idx ->
            let pageYScroll =
                if Fable.Import.JS.isNaN window.pageYOffset
                then document.documentElement.scrollTop
                else window.pageYOffset
            let item = document.querySelectorAll("table.content tr a").[idx]
            if not (isNull item) then
                let rect = item.getBoundingClientRect()
                let x = rect.left
                let y = rect.top + pageYScroll
                let w = rect.width
                createObj [ "x" ==> x; "y" ==> y; "w" ==> w ]
                |> log
            else null)
    if idx = None then
        options.showAnimationDuration <- Some 0.0
        options.hideAnimationDuration <- Some 0.0
    let gallery = photoSwipe.Create(el, U2.Case1 (box photoSwipeUIDefault), ResizeArray photos, options)
    gallery.init()
}

showGallery "latest" None
|> Promise.start