// ts2fable 0.5.2
module rec PhotoSwipe

open System
open Fable.Core
open Browser.Types

type [<AllowNullLiteral>] IExports =
    abstract PhotoSwipe: PhotoSwipeStatic

module PhotoSwipe =

    /// A specific slide in the PhotoSwipe gallery. The terms "item", "slide", and "slide object" are used interchangeably.
    type [<AllowNullLiteral>] Item =
        /// The url of this image.
        abstract src: string with get, set
        /// The width of this image.
        abstract w: float with get, set
        /// The height of this image.
        abstract h: float with get, set
        /// Internal property added by PhotoSwipe.
        abstract loadError: bool option with get, set
        /// Internal property added by PhotoSwipe.
        abstract vGap: obj option with get, set
        /// Internal property added by PhotoSwipe.
        /// This number is computed to be this item's smaller dimension divided by the larger dimension.
        abstract fitRatio: float option with get, set
        /// Internal property added by PhotoSwipe.
        abstract initialZoomLevel: float option with get, set
        /// Internal property added by PhotoSwipe.
        abstract bounds: obj option option with get, set
        /// Internal property added by PhotoSwipe.
        abstract initialPosition: obj option option with get, set

    /// Options for the base PhotoSwipe class. Derived from http://photoswipe.com/documentation/options.html
    type [<AllowNullLiteral>] Options =
        /// Start slide index. 0 is the first slide. Must be integer, not a string.
        /// 
        /// Default 0.
        abstract index: float option with get, set
        /// Function should return an object with coordinates from which initial zoom-in animation will start (or zoom-out animation will end).
        /// Object should contain three properties: x (X position, relative to document), y (Y position, relative to document), w (width of the element).
        /// Height will be calculated automatically based on size of large image.
        /// For example if you return {x:0,y:0,w:50} zoom animation will start in top left corner of your page.
        /// Function has one argument - index of the item that is opening or closing.
        /// 
        /// Default undefined.
        abstract getThumbBoundsFn: (float -> obj) option with get, set
        /// Initial zoom-in transition duration in milliseconds. Set to 0 to disable. Besides this JS option, you need also to change transition duration in PhotoSwipe CSS file:
        /// .pswp--animate_opacity,
        /// .pswp__bg,
        /// .pswp__caption,
        /// .pswp__top-bar,
        /// .pswp--has_mouse .pswp__button--arrow--left,
        /// .pswp--has_mouse .pswp__button--arrow--right{
        ///      -webkit-transition: opacity 333ms cubic-bezier(.4,0,.22,1);
        ///      transition: opacity 333ms cubic-bezier(.4,0,.22,1);
        /// }
        /// 
        /// Default 333.
        abstract showAnimationDuration: float option with get, set
        /// The same as the previous option, just for closing (zoom-out) transition.
        /// After PhotoSwipe is opened pswp--open class will be added to the root element, you may use it to apply different transition duration in CSS.
        /// 
        /// Default 333.
        abstract hideAnimationDuration: float option with get, set
        /// If set to false background opacity and image scale will be animated (image opacity is always 1).
        /// If set to true root PhotoSwipe element opacity and image scale will be animated.
        /// Enable it when dimensions of your small thumbnail don't match dimensions of large image.
        /// 
        /// Default false.
        abstract showHideOpacity: bool option with get, set
        /// Background (.pswp__bg) opacity.
        /// Should be a number from 0 to 1, e.g. 0.7.
        /// This style is defined via JS, not via CSS, as this value is used for a few gesture-based transitions.
        /// 
        /// Default 1.
        abstract bgOpacity: float option with get, set
        /// Spacing ratio between slides. For example, 0.12 will render as a 12% of sliding viewport width (rounded).
        /// 
        /// Default 0.12.
        abstract spacing: float option with get, set
        /// Allow swipe navigation to next/prev item when current item is zoomed.
        /// Option is always false on devices that don't have hardware touch support.
        /// 
        /// Default true.
        abstract allowNoPanText: bool option with get, set
        /// Maximum zoom level when performing spread (zoom) gesture. 2 means that image can be zoomed 2x from original size.
        /// Try to avoid huge values here, as too big image may cause memory issues on mobile (especially on iOS).
        /// 
        /// Default 2.
        abstract maxSpreadZoom: float option with get, set
        /// Function should return zoom level to which image will be zoomed after double-tap gesture, or when user clicks on zoom icon, or mouse-click on image itself.
        /// If you return 1 image will be zoomed to its original size.
        /// Function is called each time zoom-in animation is initiated. So feel free to return different values for different images based on their size or screen DPI.
        /// 
        /// Default is:
        /// 
        /// function(isMouseClick, item) {
        /// 
        ///      // isMouseClick          - true if mouse, false if double-tap
        ///      // item                  - slide object that is zoomed, usually current
        ///      // item.initialZoomLevel - initial scale ratio of image
        ///      //                         e.g. if viewport is 700px and image is 1400px,
        ///      //                              initialZoomLevel will be 0.5
        /// 
        ///      if(isMouseClick) {
        /// 
        ///          // is mouse click on image or zoom icon
        /// 
        ///          // zoom to original
        ///          return 1;
        /// 
        ///          // e.g. for 1400px image:
        ///          // 0.5 - zooms to 700px
        ///          // 2   - zooms to 2800px
        /// 
        ///      } else {
        /// 
        ///          // is double-tap
        /// 
        ///          // zoom to original if initial zoom is less than 0.7x,
        ///          // otherwise to 1.5x, to make sure that double-tap gesture always zooms image
        ///          return item.initialZoomLevel < 0.7 ? 1 : 1.5;
        ///      }
        /// }
        abstract getDoubleTapZoom: (bool -> Item -> float) option with get, set
        /// Loop slides when using swipe gesture.If set to true you'll be able to swipe from last to first image.
        /// Option is always false when there are less than 3 slides.
        /// This option has no relation to arrows navigation. Arrows loop is turned on permanently. You can modify this behavior by making custom UI.
        /// 
        /// Default true.
        abstract loop: bool option with get, set
        /// Pinch to close gallery gesture. The gallery’s background will gradually fade out as the user zooms out. When the gesture is complete, the gallery will close.
        /// 
        /// Default true.
        abstract pinchToClose: bool option with get, set
        /// Close gallery on page scroll. Option works just for devices without hardware touch support.
        /// 
        /// Default true.
        abstract closeOnScroll: bool option with get, set
        /// Close gallery when dragging vertically and when image is not zoomed. Always false when mouse is used.
        /// 
        /// Default true.
        abstract closeOnVerticalDrag: bool option with get, set
        /// Option allows you to predefine if mouse was used or not.
        /// Some PhotoSwipe feature depend on it, for example default UI left/right arrows will be displayed only after mouse is used.
        /// If set to false, PhotoSwipe will start detecting when mouse is used by itself, mouseUsed event triggers when mouse is found.
        /// 
        /// default false.
        abstract mouseUsed: bool option with get, set
        /// esc keyboard key to close PhotoSwipe. Option can be changed dynamically (yourPhotoSwipeInstance.options.escKey = false;).
        /// 
        /// Default true.
        abstract escKey: bool option with get, set
        /// Keyboard left or right arrow key navigation. Option can be changed dynamically (yourPhotoSwipeInstance.options.arrowKeys = false;).
        /// 
        /// Default true.
        abstract arrowKeys: bool option with get, set
        /// If set to false disables history module (back button to close gallery, unique URL for each slide). You can also just exclude history.js module from your build.
        /// 
        /// Default true.
        abstract history: bool option with get, set
        /// Gallery unique ID. Used by History module when forming URL. For example, second picture of gallery with UID 1 will have URL: http://example.com/#&gid=1&pid=2.
        /// 
        /// Default 1.
        abstract galleryUID: float option with get, set
        /// Error message when image was not loaded. %url% will be replaced by URL of image.
        /// 
        /// Default is:
        /// 
        /// <div class="pswp__error-msg"><a href="%url%" target="_blank">The image</a> could not be loaded.</div>
        abstract errorMsg: string option with get, set
        /// Lazy loading of nearby slides based on direction of movement.
        /// Should be an array with two integers, first one - number of items to preload before current image, second one - after the current image.
        /// E.g. if you set it to [1,3], it'll load 1 image before the current, and 3 images after current. Values can not be less than 1.
        /// 
        /// Default [1, 1].
        abstract preload: ResizeArray<float> option with get, set
        /// String with name of class that will be added to root element of PhotoSwipe (.pswp). Can contain multiple classes separated by space.
        abstract mainClass: string option with get, set
        /// NOTE: this property will be ignored in future versions of PhotoSwipe.
        abstract mainScrollEndFriction: float option with get, set
        /// NOTE: this property will be ignored in future versions of PhotoSwipe.
        abstract panEndFriction: float option with get, set
        /// Function that should return total number of items in gallery. Don't put very complex code here, function is executed very often.
        /// 
        /// By default it returns length of slides array.
        abstract getNumItemsFn: (unit -> float) option with get, set
        /// Will set focus on PhotoSwipe element after it's open.
        /// 
        /// Default true.
        abstract focus: bool option with get, set
        /// Function should check if the element (el) is clickable.
        /// If it is – PhotoSwipe will not call preventDefault and click event will pass through.
        /// Function should be as light is possible, as it's executed multiple times on drag start and drag release.
        /// 
        /// Default is:
        /// 
        /// function(el) {
        ///      return el.tagName === 'A';
        /// }
        abstract isClickableElement: (HTMLElement -> bool) option with get, set
        /// Controls whether PhotoSwipe should expand to take up the entire viewport.
        /// If false, the PhotoSwipe element will take the size of the positioned parent of the template. Take a look at the FAQ for more
        /// information.
        abstract modal: bool option with get, set

    type [<AllowNullLiteral>] UIFramework =
        [<Emit "$0[$1]{{=$2}}">] abstract Item: name: string -> obj option with get, set

    /// Base type for PhotoSwipe user interfaces.
    /// T is the type of options that this PhotoSwipe.UI uses.
    /// 
    /// To build your own PhotoSwipe.UI class:
    /// 
    /// (1) Write an interface for the custom UI's Options that extends PhotoSwipe.Options.
    /// (2) Write your custom class, implementing the PhotoSwipe.UI interface.
    /// (3) Pass in your custom interface to the type parameter T of the PhotoSwipe.UI interface.
    /// 
    /// Example:
    /// 
    /// // (1)
    /// interface MyUIOptions extends PhotoSwipe.Options {
    ///      foo: number;
    ///      bar: string;
    /// }
    /// 
    /// // (2) and (3)
    /// class MyUI implements PhotoSwipe.UI<MyUIOptions> {
    ///      constructor(pswp: PhotoSwipe<MyUIOptions>, framework: PhotoSwipe.UIFramework) {
    ///      }
    /// }
    /// 
    /// var pswpWithMyUI = new PhotoSwipe<MyUIOptions>(element, MyUI, items, {foo: 1, bar: "abc"});
    type [<AllowNullLiteral>] UI<'T> =
        /// Called by PhotoSwipe after it constructs the UI.
        abstract init: (unit -> unit) with get, set

/// Base PhotoSwipe class. Derived from http://photoswipe.com/documentation/api.html
type [<AllowNullLiteral>] PhotoSwipe<'T> =
    /// Current slide object.
    abstract currItem: PhotoSwipe.Item with get, set
    /// Items in this gallery. PhotoSwipe will (almost) dynamically respond to changes in this array.
    /// To add, edit, or remove slides after PhotoSwipe is opened, you just need to modify the items array.
    /// 
    /// For example, you can push new slide objects into the items array:
    /// 
    /// pswp.items.push({
    ///      src: "path/to/image.jpg",
    ///      w:1200,
    ///      h:500
    /// });
    /// 
    /// If you changed slide that is CURRENT, NEXT or PREVIOUS (which you should try to avoid) – you need to call method that will update their content:
    /// 
    /// // sets a flag that slides should be updated
    /// pswp.invalidateCurrItems();
    /// // updates the content of slides
    /// pswp.updateSize(true);
    /// 
    /// If you're using the DefaultUI, call pswp.ui.update() to update that as well. Also note:
    /// 
    /// (1) You can't reassign whole array, you can only modify it (e.g. use splice to remove elements).
    /// (2) If you're going to remove current slide – call goTo method before.
    /// (3) There must be at least one slide.
    /// (4) This technique is used to serve responsive images.
    abstract items: ResizeArray<PhotoSwipe.Item> with get, set
    /// Size of the current viewport.
    abstract viewportSize: obj with get, set
    /// The Framework. Holds utility methods.
    abstract framework: PhotoSwipe.UIFramework with get, set
    /// The ui instance constructed by PhotoSwipe.
    abstract ui: PhotoSwipe.UI<'T> with get, set
    /// The background element (with class .pswp__bg).
    abstract bg: HTMLElement with get, set
    /// The container element (with class .pswp__container).
    abstract container: HTMLElement with get, set
    /// Options for this PhotoSwipe. This object is a copy of the options parameter passed into the constructor.
    /// Some properties in options are dynamically modifiable.
    abstract options: 'T with get, set
    /// Current item index.
    abstract getCurrentIndex: unit -> float
    /// Current zoom level.
    abstract getZoomLevel: unit -> float
    /// Whether one (or more) pointer is used.
    abstract isDragging: unit -> bool
    /// Whether two (or more) pointers are used.
    abstract isZooming: unit -> bool
    /// true wehn transition between is running (after swipe).
    abstract isMainScrollAnimating: unit -> bool
    /// Initialize and open gallery (you can bind events before this method).
    abstract init: unit -> unit
    /// Go to slide by index.
    abstract goTo: index: float -> unit
    /// Go to the next slide.
    abstract next: unit -> unit
    /// Go to the previous slide.
    abstract prev: unit -> unit
    /// Update gallery size
    abstract updateSize: force: bool -> unit
    /// Close gallery. Calls destroy() after closing.
    abstract close: unit -> unit
    /// Destroy gallery (unbind listeners, free memory). Automatically called after close().
    abstract destroy: unit -> unit
    /// Zoom in/out the current slide to a specified zoom level, optionally with animation.
    abstract zoomTo: destZoomLevel: float * centerPoint: PhotoSwipeZoomToCenterPoint * speed: float * ?easingFn: (float -> float) * ?updateFn: (float -> unit) -> unit
    /// Apply zoom and pan to the current slide
    abstract applyZoomPan: zoomLevel: float * panX: float * panY: float -> unit
    /// Call this method after dynamically modifying the current, next, or previous slide in the items array.
    abstract invalidateCurrItems: unit -> unit
    /// PhotoSwipe uses very simple Event/Messaging system.
    /// It has two methods shout (triggers event) and listen (handles event).
    /// For now there is no method to unbind listener, but all of them are cleared when PhotoSwipe is closed.
    abstract listen: eventName: string * callback: (ResizeArray<obj option> -> unit) -> unit
    /// Called before slides change (before the content is changed ,but after navigation). Update UI here.
    [<Emit "$0.listen('beforeChange',$1)">] abstract listen_beforeChange: callback: (unit -> unit) -> unit
    /// Called after slides change (after content has changed).
    [<Emit "$0.listen('afterChange',$1)">] abstract listen_afterChange: callback: (unit -> unit) -> unit
    /// Called when an image is loaded.
    [<Emit "$0.listen('imageLoadComplete',$1)">] abstract listen_imageLoadComplete: callback: (float -> PhotoSwipe.Item -> unit) -> unit
    /// Called when the viewport size changes.
    [<Emit "$0.listen('resize',$1)">] abstract listen_resize: callback: (unit -> unit) -> unit
    /// Triggers when PhotoSwipe reads slide object data, which happens before content is set, or before lazy-loading is initiated.
    /// Use it to dynamically change properties of the slide object.
    [<Emit "$0.listen('gettingData',$1)">] abstract listen_gettingData: callback: (float -> PhotoSwipe.Item -> unit) -> unit
    /// Called when mouse is first used (triggers only once).
    [<Emit "$0.listen('mouseUsed',$1)">] abstract listen_mouseUsed: callback: (unit -> unit) -> unit
    /// Called when opening zoom in animation starting.
    [<Emit "$0.listen('initialZoomIn',$1)">] abstract listen_initialZoomIn: callback: (unit -> unit) -> unit
    /// Called when opening zoom in animation finished.
    [<Emit "$0.listen('initialZoomInEnd',$1)">] abstract listen_initialZoomInEnd: callback: (unit -> unit) -> unit
    /// Called when closing zoom out animation started.
    [<Emit "$0.listen('initialZoomOut',$1)">] abstract listen_initialZoomOut: callback: (unit -> unit) -> unit
    /// Called when closing zoom out animation finished.
    [<Emit "$0.listen('initialZoomOutEnd',$1)">] abstract listen_initialZoomOutEnd: callback: (unit -> unit) -> unit
    /// Allows overriding vertical margin for individual items.
    /// 
    /// Example:
    /// 
    /// pswp.listen('parseVerticalMargin', function(item) {
    ///      var gap = item.vGap;
    /// 
    ///      gap.top = 50; // There will be 50px gap from top of viewport
    ///      gap.bottom = 100; // and 100px gap from the bottom
    /// });
    [<Emit "$0.listen('parseVerticalMargin',$1)">] abstract listen_parseVerticalMargin: callback: (PhotoSwipe.Item -> unit) -> unit
    /// Called when the gallery starts closing.
    [<Emit "$0.listen('close',$1)">] abstract listen_close: callback: (unit -> unit) -> unit
    /// Gallery unbinds events (triggers before closing animation).
    [<Emit "$0.listen('unbindEvents',$1)">] abstract listen_unbindEvents: callback: (unit -> unit) -> unit
    /// Called after the gallery is closed and the closing animation finishes.
    /// Clean up your stuff here.
    [<Emit "$0.listen('destroy',$1)">] abstract listen_destroy: callback: (unit -> unit) -> unit
    /// Allow to call preventDefault on down and up events.
    [<Emit "$0.listen('preventDragEvent',$1)">] abstract listen_preventDragEvent: callback: (MouseEvent -> bool -> obj -> unit) -> unit
    /// Triggers eventName event with args passed through to listeners.
    abstract shout: eventName: string * [<ParamArray>] args: ResizeArray<obj option> -> unit

type [<AllowNullLiteral>] PhotoSwipeZoomToCenterPoint =
    abstract x: float with get, set
    abstract y: float with get, set

/// Base PhotoSwipe class. Derived from http://photoswipe.com/documentation/api.html
type [<AllowNullLiteral>] PhotoSwipeStatic =
    /// Constructs a PhotoSwipe.
    /// 
    /// Note: By default Typescript will not correctly typecheck the options parameter. Make sure to
    /// explicitly annotate the type of options being passed into the constructor like so:
    /// 
    /// new PhotoSwipe<PhotoSwipeUI_Default.Options>( element, PhotoSwipeUI_Default, items, options );
    /// 
    /// It accepts 4 arguments:
    /// 
    /// (1) PhotoSwipe element (it must be added to DOM).
    /// (2) PhotoSwipe UI class. If you included default photoswipe-ui-default.js, class will be PhotoSwipeUI_Default. Can be "false".
    /// (3) Array with objects (slides).
    /// (4) Options.
    [<Emit "new $0($1...)">] abstract Create: pswpElement: HTMLElement * uiConstructor: U2<obj, bool> * items: ResizeArray<PhotoSwipe.Item> * options: 'T -> PhotoSwipe<'T>
