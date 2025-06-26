const channel = new BroadcastChannel('encounter_channel');

window.broadcastMessage = (message) => {
    if ('title' in message) {
        channel.postMessage(message);
        console.log("Sent Encounter => ", message.id, " ", message.title);
    }
};

window.onBroadcastMessage = (dotNetHelper) => {
    channel.onmessage = (event) => {
        if ('title' in event.data) {
            console.log("Recieved Encounter => ", event.data.id, " ", event.data.title);
            dotNetHelper.invokeMethodAsync('ReceiveEncounter', event.data.id);
        }
    };
};