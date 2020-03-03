var AnimatedComponent = AnimatedComponent || {};
AnimatedComponent.animationend = function (element, dotNet) {
    element.addEventListener('animationend', function() { dotNet.invokeMethodAsync("OnAnimationEnd") });
};